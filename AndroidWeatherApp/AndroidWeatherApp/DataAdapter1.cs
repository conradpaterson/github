
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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;


namespace AndroidWeatherApp

{

	public class DataAdapter1 : BaseAdapter<Response.RootObject> {

		List<Response.RootObject> items;

		Activity context;
		public DataAdapter1(Activity context, List<Response.RootObject> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Response.RootObject this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.OpeningScreenCustomRow, null);

			view.FindViewById<TextView>(Resource.Id.tvOSCRCity).Text = item.name;
            view.FindViewById<TextView>(Resource.Id.tvOSCRCurrentTemp).Text = item.main.temp.ToString();
            view.FindViewById<TextView>(Resource.Id.tvOSCRWeatherCondition).Text = item.weather[0].description.ToString();

            if (item.weather[0].icon != null)
            {
                string myUrl = "http://openweathermap.org/img/w/" + item.weather[0].icon + ".png";

                var imageBitmap = GetImageBitmapFromUrl(myUrl);
                view.FindViewById<ImageView>(Resource.Id.ivOSCRWeatherIcon).SetImageBitmap(imageBitmap);
            }
			return view;
       
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
