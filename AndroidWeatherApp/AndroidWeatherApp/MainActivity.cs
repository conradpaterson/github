using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Locations;
using Android.Util;

namespace AndroidWeatherApp
{
    [Activity(Label = "AndroidWeatherApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity , ILocationListener
    {
        String City;
        ListView LVCity;

        LocationManager locMgr;
        double myLat;
        double myLong;
      

        String[] Cities = { "Auckland", "Hamilton", "Wellington", "Christchurch" };
        RESTHandler objRest;

        List<ResponseCurrent.RootObject> myCities = new List<ResponseCurrent.RootObject>();
        ResponseCurrent.RootObject myCity;
        private string tag;

        Button ListLocation;
        Button CurrentLocation;

        ListWrapper _savedInstance;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CurrentLocation = FindViewById<Button>(Resource.Id.btnCurrentLocation);
            ListLocation = FindViewById<Button>(Resource.Id.btnListLocation);
            LVCity = FindViewById<ListView>(Resource.Id.lvCity);

            CurrentLocation.Click += CurrentLocation_Click;
            ListLocation.Click += ListLocation_Click;
            LVCity.ItemClick += LVCity_ItemClick;

            //get reference to location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            var citiesWrapper = LastNonConfigurationInstance as ListWrapper;

            if (citiesWrapper != null)
                PopulateCityList(citiesWrapper.theList);
           
        }

        public class ListWrapper : Java.Lang.Object
        {
            public List<ResponseCurrent.RootObject> theList { get; set; }
        }

        public override Java.Lang.Object OnRetainNonConfigurationInstance()
        {
            base.OnRetainNonConfigurationInstance();
            return _savedInstance;
        }

        public void PopulateCityList(List<ResponseCurrent.RootObject> aList)
        {
            //myCities.Clear();

            myCities = aList;

            LVCity.ClearChoices();
            LVCity.Adapter = new DataAdapterCurrent(this, myCities);


            //save the saved instance state
            _savedInstance = new ListWrapper { theList = myCities };
        }

        private void ListLocation_Click(object sender, EventArgs e)
        {
            InitializeListLocationListView();

            //throw new NotImplementedException();
        }

        private void CurrentLocation_Click(object sender, EventArgs e)
        {
            string Provider = LocationManager.GpsProvider;

            if (locMgr.IsProviderEnabled(Provider))
            {
                locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
            }
            else
            {
                Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
            }

           

            objRest = new RESTHandler(@"http://api.openweathermap.org/data/2.5/weather?lat=" + myLat + "&lon=" + myLong + "&units=metric&appid=5253da01ea069ebc06e2a854b436f080");
            var Response = objRest.ExecuteCurrentRequest();

            myCities.Clear();
            myCities.Add(Response);

            LVCity.ClearChoices();
            LVCity.Adapter = new DataAdapterCurrent(this, myCities);

            //save the saved instance state
            _savedInstance = new ListWrapper { theList = myCities };
        }

        //protected override void OnSaveInstanceState(Bundle outState)
        //{
        //    outState.PutSerializable("cityList", myCities.;
        //    base.OnSaveInstanceState(outState);
        //}

        protected override void OnStop()
        {
            base.OnStop();

            //save the saved instance state
            _savedInstance = new ListWrapper { theList = myCities };
        }

        protected override void OnResume()
        {
            base.OnResume();
            string Provider = LocationManager.GpsProvider;

            if (locMgr.IsProviderEnabled(Provider))
            {
                locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
            }
            else
            {
                Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
            }

            //repopulate any saved data
            var citiesWrapper = LastNonConfigurationInstance as ListWrapper;

            if (citiesWrapper != null)
                PopulateCityList(citiesWrapper.theList);

        }

        public void InitializeListLocationListView()
        {
            myCities.Clear();

            for (int i = 0; i < Cities.Length; i++)
            {
                City = Cities[i];

                objRest = new RESTHandler(@"http://api.openweathermap.org/data/2.5/weather?q=" + City + ",NZ&units=metric&appid=5253da01ea069ebc06e2a854b436f080");
                var Response =  objRest.ExecuteCurrentRequest();

                myCities.Add(Response);
            }

            LVCity.ClearChoices();
            LVCity.Adapter = new DataAdapterCurrent(this, myCities);

            //save the saved instance state
            _savedInstance = new ListWrapper { theList = myCities };
        }

        public void LVCity_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            myCity = myCities[e.Position];
            string theCity = myCity.name;
            string theTemp = myCity.main.temp.ToString();
            string theWeatherCondition = myCity.weather[0].description.ToString();
            string theWeatherIconUrl = "http://openweathermap.org/img/w/" + myCity.weather[0].icon + ".png";

            Intent myIntent = new Intent(this, typeof(IndividualCityActivity));
            myIntent.PutExtra("City", theCity);
            myIntent.PutExtra("Temp", theTemp);
            myIntent.PutExtra("Desc", theWeatherCondition);
            myIntent.PutExtra("WeatherIcon", theWeatherIconUrl);

            StartActivity(myIntent);          
        }

        public void OnLocationChanged(Location location)
        {
            myLat = location.Latitude;
            myLong = location.Longitude;



            //throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }

      
    }
}

