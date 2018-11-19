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
using CapstoneProject.Data;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        private async Task<Food> StorePicture(Food food, IFormFile picture, bool IsIngredientsPicture)
        {
            if (picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await picture.CopyToAsync(stream);
                    if (IsIngredientsPicture)
                    {
                        food.IngredientsPicture = stream.ToArray();
                    }
                    else
                    {
                        food.ProductPicture = stream.ToArray();
                    }
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
            food = await StorePicture(food, picture, true);
            var nonVeganFoods = _context.NonVeganFoods.Select(f => f.Keyword).ToList();
            // Instantiates a client
            List<string> ingredients = new List<string>();
            List<string> foundProblems = new List<string>();
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
                if (nonVeganFoods.Contains(i))
                {
                    food.IsVegan = false;
                    foundProblems.Add(i);
                }
                // Questionabe foods check?
            }
            return RedirectToAction("IngredientsResults", "Home", new { foodEntry = food, wordsFound = foundProblems });            
        }

        public IActionResult IngredientsResults(Food foodEntry, List<string> wordsFound)
        {
            FoodViewModel newFood = new FoodViewModel() { Food = foodEntry, KeyWords = wordsFound, IsVegan = false };
            if (wordsFound.Count == 0)
            {
                newFood.IsVegan = true;
            }
            return View(newFood);
        }
        public IActionResult SaveFood(FoodViewModel foodModel)
        {            
            return View(foodModel.Food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveFood([Bind("Id,Name,IngredientsPicture,ProductPicture,IsVegan,Notes")] Food newFood, IFormFile picture)
        {            
            newFood = await StorePicture(newFood, picture, true);
            if (ModelState.IsValid)
            {
                await _context.Food.AddAsync(newFood);                
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); //Make these lines a function?
            var standardUserId = _context.StandardUsers.Where(s => s.ApplicationUserId == userId).Select(u => u.Id).Single();
            UserFood userFood = new UserFood() { StandardUserId = standardUserId, Food = newFood };
            await _context.UserFoods.AddAsync(userFood);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserFoods");
        }

        public IActionResult UserFoods()
        {
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
