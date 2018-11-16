using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class VeganIngredient
    {
        [Key]
        public string Id { get; set; }
        public string OriginalFood { get; set; }
        public string NewFood { get; set; }
        public bool IsIngredient { get; set; }
    }
}
