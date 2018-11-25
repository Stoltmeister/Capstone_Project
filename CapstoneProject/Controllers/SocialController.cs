using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CapstoneProject.Controllers
{
    public class SocialController : Controller
    {
        public static HttpClient client;
        public static string newsFullRequest;
        public static string newsApiFirst;
        public static string newsApiSecond;

        public SocialController()
        {
            client = new HttpClient();
            newsApiFirst = "https://newsapi.org/v2/everything?q=\"vegan\"&from=";
            newsApiSecond = "-00&sortBy=popularity&language=en&apiKey=" + ApiKeys.NewsApiKey;
            newsFullRequest = newsApiFirst + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + newsApiSecond;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult News()
        {
            var articles = GetRootObject(newsFullRequest).Result.articles;
            List<Article> veganArticles = new List<Article>();
            foreach (Article a in articles)
            {
                var keyWords = a.content + " " + a.description + " " + a.title;
                if (keyWords.Contains("vegan") || keyWords.Contains("Vegan"))
                {
                    veganArticles.Add(a);
                }
            }
            // filter articles further
            return View(veganArticles);
        }


        static async Task<RootObject> GetRootObject(string path)
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