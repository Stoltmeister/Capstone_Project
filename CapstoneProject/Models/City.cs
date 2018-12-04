using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class City
    {
        [Display(Name = "City")]
        public string CitySearchKeyword { get; set; }
        public int CityId { get; set; }
    }
}
