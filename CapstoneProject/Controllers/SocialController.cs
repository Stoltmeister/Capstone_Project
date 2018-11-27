using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CapstoneProject.Controllers
{
    public class SocialController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static HttpClient client;
        public static string newsFullRequest;
        public static string newsApiFirst;
        public static string newsApiSecond;

        public SocialController(ApplicationDbContext context)
        {
            _context = context;
            client = new HttpClient();
            newsApiFirst = "https://newsapi.org/v2/everything?q=\"vegan\"&from=";
            newsApiSecond = "-00&sortBy=popularity&language=en&apiKey=" + ApiKeys.NewsApiKey;
            newsFullRequest = newsApiFirst + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + newsApiSecond;
        }

        public IActionResult Index()
        {
            return View("RecommendedSocials");
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
        private string GetStandardUserId()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var standardUserId = _context.StandardUsers.Where(s => s.ApplicationUserId == userId).Select(u => u.Id).Single();
            return standardUserId;
        }

        
        public IActionResult WeeklyEmail()
        {
            return View();
        }

        public IActionResult GetCharge()
        {
            return View("StripeCharge");
        }
        [HttpPost]
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });
            // ADD Content to Sponsered Section
            return View("ChargeConfirmation");
        }
        [HttpPost]
        public async Task<IActionResult> WeeklyEmail(string email)
        {
            // only logged in users
            var random = new Random();
            var allFoods = _context.VeganFoods.ToList();
            var randomFood = allFoods.ElementAt(random.Next(allFoods.Count() - 1)); //check for empty
            string subject = "Weekly Vegan Dish";
            string body = "Your food to try this week is " + randomFood.Name + "! " + randomFood.Description + " Check out the recipe here: " + randomFood.URL;
            await Sendgrid.SendMail(email, subject, body);
            var user = _context.StandardUsers.Where(u => u.Id == GetStandardUserId()).FirstOrDefault();
            user.IsGettingWeeklyEmail = true;
            await _context.SaveChangesAsync();
            return View("EmailConfirmation");

        }
    }
}