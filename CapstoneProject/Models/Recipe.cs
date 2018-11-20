using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Recipe
    {        
        public string id { get; set; }
        public string recipeName { get; set; }
        [NotMapped]
        public List<string> imageUrlsBySize { get; set; }
    }
}
