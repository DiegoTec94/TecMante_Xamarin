using AppXamarin.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppXamarin.Services
{
    public interface IJSONPlaceholder
    {
        List<Posts> GetPosts();
    }
    public class JSONPlaceholder :BaseService, IJSONPlaceholder
    {
        public  List<Posts> GetPosts()
        {
            Request = Build($"posts", Method.GET, DataFormat.Json);

            Response = Client?.Execute(Request);

            return Response.IsSuccessful ? JsonConvert.DeserializeObject<List<Posts>>(Response.Content) : null;
        }
    }
}
