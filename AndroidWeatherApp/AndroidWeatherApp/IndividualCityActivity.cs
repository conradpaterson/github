using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.Graphics;
using System.Net;

namespace AndroidWeatherApp
{
    [Activity(Label = "IndividualCityActivity")]
    public class IndividualCityActivity : Activity
    {
        string City;
        string Temp;
        string WeatherCondition;
        string WeatherIconUrl;

        TextView CityName;
        TextView dateTime1;
        TextView dateTime2;
        TextView CurrentTemperature;
        TextView CurrentWeatherCondition;
        ImageView mainIcon;

        RESTHandler objRest;
        Response16Day.RootObject myResponse;
        List<Response16Day.List> myDays;

        ListView dayList;

        DateTime cityDateTime = new DateTime();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.IndividualCityDailyLayout);

            City = Intent.GetStringExtra("City");
            Temp = Intent.GetStringExtra("Temp");
            WeatherCondition = Intent.GetStringExtra("Desc");
            WeatherIconUrl = Intent.GetStringExtra("WeatherIcon");

            mainIcon = FindViewById<ImageView>(Resource.Id.ivMainIcon);
            var imageBitmap = GetImageBitmapFromUrl(WeatherIconUrl);
            mainIcon.SetImageBitmap(imageBitmap);

            CityName = FindViewById<TextView>(Resource.Id.tvICLCity);
            dateTime1 = FindViewById<TextView>(Resource.Id.tvICLDateTime);
            dateTime2 = FindViewById<TextView>(Resource.Id.tvICDCRDate);
            CurrentTemperature = FindViewById<TextView>(Resource.Id.tvICLCurrentTemperature);
            CurrentWeatherCondition = FindViewById<TextView>(Resource.Id.tvICLCurrentWeatherCondition);

            cityDateTime = System.DateTime.Now;
            dateTime1.Text = cityDateTime.ToString();
            //dateTime2.Text = cityDateTime.ToString();

            CityName.Text = City;
            Temp = Temp + " \u2103";
            CurrentTemperature.Text = Temp;
            CurrentWeatherCondition.Text = WeatherCondition;

            dayList = FindViewById<ListView>(Resource.Id.lvDayList);

            PerformInitializationOfList();
                     
        }

        public async void PerformInitializationOfList()
        {
            objRest = new RESTHandler(@"http://api.openweathermap.org/data/2.5/forecast/daily?q=" + City + ",NZ&units=metric&cnt=16&appid=5253da01ea069ebc06e2a854b436f080");

            myResponse = await objRest.Execute16DayRequestAsync();
            myDays = myResponse.list;
            dayList.Adapter = new DataAdapter16Day(this, myDays);

        }

        public Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            if (!(url == "null"))
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

            return imageBitmap;
        }
    }
}