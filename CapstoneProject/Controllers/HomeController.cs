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

        private string GetStandardUserId()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var standardUserId = _context.StandardUsers.Where(s => s.ApplicationUserId == userId).Select(u => u.Id).Single();
            return standardUserId;
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
            await _context.Food.AddAsync(food);
            await _context.SaveChangesAsync();
            var nonVeganFoods = _context.NonVeganFoods.Select(f => f.Keyword).ToList();
            // Instantiates a client
            List<string> ingredients = new List<string>();
            List<string> foundProblems = new List<string>();
            //var client = ImageAnnotatorClient.Create();
            //// Load the image file into memory
            //var image = Image.FromBytes(food.IngredientsPicture);
            //// Performs label detection on the image file
            //var response = client.DetectText(image);
            //foreach (var annotation in response)
            //{
            //    if (annotation.Description != null)
            //        ingredients.Add(annotation.Description.Trim(new char[] { ' ', '*', '.', '[', ']', ',', '(', ')' }));
            //}
            foreach (string i in ingredients)
            {
                if (_context.NonVeganFoods.Any(f => f.Keyword.ToLower() == i.ToLower()))
                {
                    food.IsVegan = false;
                    foundProblems.Add(i);
                }
                // Questionabe foods check?
            }
            return RedirectToAction("IngredientsResults", "Home", new { foodID = food.Id, wordsFound = foundProblems });
        }

        public async Task<IActionResult> IngredientsResults(string foodID, List<string> wordsFound)
        {
            var foodEntry = _context.Food.Where(f => f.Id == foodID).SingleOrDefault();
            FoodViewModel newFood = new FoodViewModel() { Food = foodEntry, KeyWords = wordsFound, FoodId = foodEntry.Id };
            if (wordsFound.Count == 0)
            {
                foodEntry.IsVegan = true;
                await _context.SaveChangesAsync();
                newFood.Food.IsVegan = true;
            }
            else
            {
                newFood.KeyWords = newFood.KeyWords.Distinct().ToList();
            }
            return View(newFood);
        }

        public IActionResult SaveFood(string foodId)
        {
            var food = _context.Food.Where(f => f.Id == foodId).Single();
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveFood([Bind("Id,Name,IngredientsPicture,ProductPicture,IsVegan,Notes")] Food newFood, IFormFile picture)
        {
            var savedFood = _context.Food.Where(f => f.Id == newFood.Id).SingleOrDefault();
            using (var stream = new MemoryStream())
            {
                await picture.CopyToAsync(stream);
                savedFood.ProductPicture = stream.ToArray();
            }
            savedFood.Notes = newFood.Notes;
            savedFood.Name = newFood.Name;
            UserFood userFood = new UserFood() { StandardUserId = GetStandardUserId(), Food = savedFood };
            await _context.UserFoods.AddAsync(userFood);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserFoods");
        }

        public IActionResult UserFoods()
        {
            var userFoods = _context.UserFoods.Where(u => u.StandardUserId == GetStandardUserId()).Select(f => f.Food).ToList();
            return View(userFoods);
        }

        public IActionResult AllFoods()
        {
            var allFoods = _context.UserFoods.Select(f => f.Food).ToList();
            return View(allFoods);
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
