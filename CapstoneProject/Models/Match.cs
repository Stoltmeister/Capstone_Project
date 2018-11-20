using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Match
    {        
        public List<string> ingredients { get; set; }
        public string id { get; set; }
        public List<string> smallImageUrls { get; set; }
        [Display(Name ="Name")]
        public string recipeName { get; set; }        
    }
}
