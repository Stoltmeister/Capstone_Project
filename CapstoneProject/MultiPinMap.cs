using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject
{
    public class MultiPinMap : ViewComponent
    {
        private readonly ApplicationDbContext db;
        public MultiPinMap(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<Restaurant2> restaurants)
        {
            return View("/Views/Shared/Components/MultiPinMap.cshtml", await GetPinDataAsync(restaurants));
        }

        private async Task<List<PinData>> GetPinDataAsync(List<Restaurant2> restaurants)
        {
            var output = new List<PinData>();

            foreach (Restaurant2 r in restaurants)
            {
                try
                {
                    output.Add(new PinData("https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png", r));
                }
                catch (NullReferenceException)
                {
                    continue;
                }
            }

            return output;

        }

        public class PinData
        {
            public string IconUrl { get; set; }
            //public string Label { get; set; }
            public string RestaurantName { get; set; }
            public string Address { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public string Url { get; set; }

            public PinData(string iconUrl, Restaurant2 restaurant)
            {               
                IconUrl = iconUrl;
                //Label = label;
                RestaurantName = restaurant.name;                
                Address = restaurant.Location.address;
                Latitude = float.Parse(restaurant.Location.latitude);
                Longitude = float.Parse(restaurant.Location.longitude);
                Url = restaurant.url;
            }
        }
    }
}
