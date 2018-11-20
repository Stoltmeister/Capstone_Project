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
using System.Net.Http;

namespace CapstoneProject.Controllers
{
    public class RecipeController : Controller
    {
        public static HttpClient client = new HttpClient();
        public string allRecipesPath;
        public string recipePath;

        public RecipeController(HttpClient _client)
        {
            client = _client;
            allRecipesPath = "http://api.yummly.com/v1/api/recipes?_app_id=28617fc5&_app_key=df6233710c74240b01112e7d72bb51a7&your _search_parameters=%26q=vegan%26allowedDiet[]=390^Vegan";
            recipePath = "http://api.yummly.com/v1/api/recipe/RECIPE-ID?_app_id=28617fc5&_app_key=df6233710c74240b01112e7d72bb51a7"
        }
        public IActionResult Index()
        {
            return View();
        }

        static async Task<Recipe> GetRecipeAsync(string path)
        {
            Recipe recipe = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                recipe = await response.Content.ReadAsAsync<Recipe>();
            }
            return recipe;
        }
    }
}