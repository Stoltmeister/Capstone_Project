﻿using System;
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
using Newtonsoft.Json;

namespace CapstoneProject.Controllers
{
    public class RecipeController : Controller
    {
        public static HttpClient client;
        public static string allRecipesPath;
        public static string recipePathFirst;
        public static string recipePathSecond;

        public RecipeController()
        {
            client = new HttpClient();
            allRecipesPath = "http://api.yummly.com/v1/api/recipes?_app_id=28617fc5&_app_key=df6233710c74240b01112e7d72bb51a7&your_search_parameters=%26q=vegan%26allowedDiet[]=390^Vegan";
            recipePathFirst = "http://api.yummly.com/v1/api/recipe/";
            recipePathSecond = "?_app_id=28617fc5&_app_key=df6233710c74240b01112e7d72bb51a7"; 
        }
        public IActionResult Index()
        {
            var allRecipes = GetAllRecipes(allRecipesPath).Result;            
            return View(allRecipes.matches);
        }

        public IActionResult GetRecipe(string id)
        {
            var url = GetRecipeUrl(id).Result;
            return Redirect(url);
        }

        static async Task<string> GetRecipeUrl(string id)
        {
            string fullRecipePath = recipePathFirst + id + recipePathSecond;            
            HttpResponseMessage response = await client.GetAsync(fullRecipePath);
            RootObject recipeUrlHolder = new RootObject();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                recipeUrlHolder = JsonConvert.DeserializeObject<RootObject>(result); 
            }
            return recipeUrlHolder.attribution.url;
        }

        static async Task<RootObject> GetAllRecipes(string path)
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