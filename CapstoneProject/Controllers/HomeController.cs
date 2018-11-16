using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.Models;
using Google.Cloud.Vision.V1;


namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\ArtofShell\Google\Vision\GoogleCred.json");
            // Instantiates a client
            List<string> tags = new List<string>();
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromFile("wwwroot/images/poster.jpg");
            // Performs label detection on the image file
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    tags.Add(annotation.Description);
            }
            ViewBag.Tags = tags;
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
