using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject
{
    public static class AllVeganSubstitutions
    {
        public static string source = "https://www.vegkitchen.com/vegan-substitutions/";
        public static List<string> foodsToSub = new List<string>() { "Milk", "Eggs", "Cheese", "Meat", "Butter", "Honey", "Chocolate", "Ice Cream", "Gelatin", "Mayo", "Beef/Chicken stock"};
        public static List<string> veganSubs = new List<string>() { "Soy, Rice, or Nut Milks", "Tofu Scramble", "Vegan Cheeses (Homeade Nut Cheese)", "Veggie meats of all kinds, Seitan, and Lentils", "Vegan Butter, Olive Oil", "Maple Syrup or Agave Nectar",
        "Non-dairy chocolate", "Non-dairy Ice creams (traditional and fruity styles of all kinds)", "agar flakes/powder", "Store Vegan Mayo or Homemade", "Vegetable Stock"};
    }
}
