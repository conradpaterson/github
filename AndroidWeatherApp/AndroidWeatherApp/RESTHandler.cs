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
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AndroidWeatherApp
{
    class RESTHandler
    {
        private string url;
        private IRestResponse response;
        private RestRequest request;

        public RESTHandler()
        {
            url = "";
        }

        public RESTHandler(string lurl)
        {
            url = lurl;
            request = new RestRequest();
        }

        public void AddParameter(string name, string value)
        {
            if (request != null)
            {
                request.AddParameter(name, value);
            }
        }

        public ResponseCurrent.RootObject ExecuteCurrentRequest()
        {
            var client = new RestClient(url);

            response = client.Execute(request);

            ResponseCurrent.RootObject objRoot = new ResponseCurrent.RootObject();
            objRoot = JsonConvert.DeserializeObject<ResponseCurrent.RootObject>(response.Content);

            return objRoot;
        }

        public Response16Day.RootObject Execute16DayRequest()
        {
            var client = new RestClient(url);

            response = client.Execute(request);

            Response16Day.RootObject objRoot = new Response16Day.RootObject();
            objRoot = JsonConvert.DeserializeObject<Response16Day.RootObject>(response.Content);

            return objRoot;
        }

        public Response5Day.RootObject Execute5DayRequest()
        {
            var client = new RestClient(url);

            response = client.Execute(request);

            Response5Day.RootObject objRoot = new Response5Day.RootObject();
            objRoot = JsonConvert.DeserializeObject<Response5Day.RootObject>(response.Content);

            return objRoot;
        }

        public async Task<ResponseCurrent.RootObject> ExecuteCurrentRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            ResponseCurrent.RootObject objRoot = new ResponseCurrent.RootObject();
            objRoot = JsonConvert.DeserializeObject<ResponseCurrent.RootObject>(response.Content);

            return objRoot;
        }

        public async Task<Response16Day.RootObject> Execute16DayRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            Response16Day.RootObject objRoot = new Response16Day.RootObject();
            objRoot = JsonConvert.DeserializeObject<Response16Day.RootObject>(response.Content);

            return objRoot;
        }

        public async Task<Response5Day.RootObject> Execute5DayRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            Response5Day.RootObject objRoot = new Response5Day.RootObject();
            objRoot = JsonConvert.DeserializeObject<Response5Day.RootObject>(response.Content);

            return objRoot;
        }
    }
}