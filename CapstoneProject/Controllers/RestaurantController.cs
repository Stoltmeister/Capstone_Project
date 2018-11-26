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
        public static string vegRestaurantsRequestFirst = "https://developers.zomato.com/api/v2.1/search?entity_id=";
        public static string vegRestaurantsRequestSecond = "&entity_type=city&cuisines=308";

        public RestaurantController()
        {
            client = new HttpClient();            
            client.DefaultRequestHeaders.Add("user-key", ApiKeys.ZomatoKey); //test
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
            string vegRestaurantsRequestFull = vegRestaurantsRequestFirst + city.CityId + vegRestaurantsRequestSecond;
            var rootObject = GetVegRestaurants(vegRestaurantsRequestFull);
            var restaurants = rootObject.Result.restaurants.Select(r => r.restaurant).ToList();
            return View("RestaurantList", restaurants); // 
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

        static async Task<RootObject> GetVegRestaurants(string path)
        {
            RootObject allMatches = new RootObject();
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //Recipe recipe = await response.Content.ReadAsAsync<Recipe>();
                allMatches = JsonConvert.DeserializeObject<RootObject>(result); //response.Content.ReadAsAsync<Recipe>();
            }
            return allMatches;
        }
    }
}