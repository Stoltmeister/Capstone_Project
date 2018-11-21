using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CapstoneProject.Controllers
{
    public class RestaurantController : Controller
    {
        public static HttpClient client;
        public static string cityRequest = "https://developers.zomato.com/api/v2.1/cities?q=";

        public RestaurantController()
        {
            client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("user-key", "0261361a94af6518badc3cb536e33acd");
            client.DefaultRequestHeaders.Add("user-key", "0261361a94af6518badc3cb536e33acd");
        }

        public IActionResult Index()
        {
            City city = new City(); 
            return View(city);
        }
        [HttpPost]
        public IActionResult Index([Bind("CitySearchKeyword")] City city)
        {
            city.CityId = GetCityId(city.CitySearchKeyword).Result;
            return View();
        }

        static async Task<int> GetCityId(string searchKeyWord)
        {
            int cityId = 0;
            string fullCityRequestPath = cityRequest + searchKeyWord;
            HttpResponseMessage response = await client.GetAsync(fullCityRequestPath);
            RootObject cityIdHolder = new RootObject();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cityIdHolder = JsonConvert.DeserializeObject<RootObject>(result);
                cityId = cityIdHolder.location_suggestions.FirstOrDefault().id;
            }
            return cityId;
        }
    }
}