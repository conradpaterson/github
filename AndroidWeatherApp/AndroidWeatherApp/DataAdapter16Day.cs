using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using System;

namespace AndroidWeatherApp
{

    public class DataAdapter16Day : BaseAdapter<Response16Day.List>
{

    List<Response16Day.List> items;

    Activity context;

    public DataAdapter16Day(Activity context, List<Response16Day.List> items)
        : base()
    {
        this.context = context;
        this.items = items;
    }
    public override long GetItemId(int position)
    {
        return position;
    }
    public override Response16Day.List this[int position]
    {
        get { return items[position]; }
    }
    public override int Count
    {
        get { return items.Count; }
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
            if (position == 0)
            { return null; }
            else
            {

                var item = items[position];
                View view = convertView;
                if (view == null)
                    // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.IndividualCityDailyCustomRow, null);

                //DateTime nowTime = new DateTime();
                //String dateString = nowTime.ToString();

                //getting the dt epoch time and converting it to a readable string
                string theDate = epoch2string(item.dt);

                string myString1 = item.temp.max.ToString();
                string myString2 = myString1 + " \u2103";

                string myString3 = item.temp.min.ToString();
                string myString4 = myString3 + " \u2103";

                view.FindViewById<TextView>(Resource.Id.tvICDCRDate).Text = theDate;
                view.FindViewById<TextView>(Resource.Id.tvICDCRWeatherCondition).Text = item.weather[0].description.ToString();
                view.FindViewById<TextView>(Resource.Id.tvICDCRMaxTemp).Text = myString2;
                view.FindViewById<TextView>(Resource.Id.tvICDCRMinTemp).Text = myString4;
                view.FindViewById<TextView>(Resource.Id.tvICDCRWindSpeed).Text = item.speed + " m/s at";
                view.FindViewById<TextView>(Resource.Id.tvICDCRDegree).Text = item.deg + "\u00b0";


                if (item.weather[0].icon != null)
                {
                    string myUrl = "http://openweathermap.org/img/w/" + item.weather[0].icon + ".png";

                    var imageBitmap = GetImageBitmapFromUrl(myUrl);
                    view.FindViewById<ImageView>(Resource.Id.ivICDCRIcon).SetImageBitmap(imageBitmap);
                }
                return view;
            }
    }
     public string epoch2string(int epoch)
     {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToShortDateString();
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
