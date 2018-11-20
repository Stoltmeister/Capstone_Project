using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class FoodViewModel
    {
        public Food Food { get; set; }
        public List<string> KeyWords { get; set; }
        public string FoodId { get; set; }
    }
}
