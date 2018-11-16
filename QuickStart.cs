﻿using Google.Cloud.Vision.V1;
using System;

namespace GoogleCloudSamples
{
    public static class QuickStart
    {
        public static void RunGoogle()
        {
            // Instantiates a client
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromFile("wakeupcat.jpg");
            // Performs label detection on the image file
            var response = client.DetectLabels(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Console.WriteLine(annotation.Description);
            }
            Console.ReadLine();
        }
    }
}