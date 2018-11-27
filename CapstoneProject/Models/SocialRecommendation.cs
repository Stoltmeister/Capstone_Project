using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class SocialRecommendation
    {
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string LinksDescription { get; set; }
        public List<string> Links { get; set; }
    }
}
