using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.Models;
using Google.Cloud.Vision.V1;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private async Task<Food> StorePicture(Food food, IFormFile picture)
        {
            if (picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await picture.CopyToAsync(stream);
                    food.IngredientsPicture = stream.ToArray();
                }                
            }
            return food;
        }
            public IActionResult Index()
        {
            
            return RedirectToAction("CheckIngredients");
            return View();
        }

        public IActionResult CheckIngredients()
        {
            Food newFood = new Food();
            return View(newFood);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIngredients(Food food, IFormFile picture)
        {
            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            food = await StorePicture(food, picture);
            // Instantiates a client
            List<string> ingredients = new List<string>();
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromBytes(food.IngredientsPicture);
            // Performs label detection on the image file
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    ingredients.Add(annotation.Description);
            }
            foreach (string i in ingredients)
            {
                
            }
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
