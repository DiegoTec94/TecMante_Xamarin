using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppXamarin.Services
{
    public abstract class BaseService
    {
        protected static object lockService = new object();
        protected RestClient Client => new RestClient(AppConstants.WebApiUrl);
        protected RestRequest Request { get; set; }
        protected IRestResponse Response { get; set; }


        protected RestRequest Build(string route, Method method, DataFormat dataFormat = DataFormat.Json)
        {
            var request = new RestRequest(route, method, dataFormat);

            request.AddHeader("cache-control", "no-cache");

            return request;
        }
    }
}
