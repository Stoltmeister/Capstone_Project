using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class VeganSubstitutes
    {
        [Key]
        public string Id { get; set; }
        public string NonVeganIngredient { get; set; }
        public List<string> VeganSubs { get; set; }
    }
}
