using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Sponsor
    {
        [Key]
        public string id { get; set; }
        [Display(Name = "Page/Content Title")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = " Sponsered Content Link")]
        public string ContentUrl { get; set; }
        [Display(Name = "Content Image Link")]
        public string ImageUrl { get; set; }
        public DateTime SponsorDay { get; set; }
    }
}
