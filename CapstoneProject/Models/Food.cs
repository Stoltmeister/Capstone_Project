using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Food
    {
        [Key]
        public string Id { get; set; }
        public byte[] IngredientsPicture { get; set; }
        [Display(Name ="Food Picture")]
        public byte[] ProductPicture { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool IsVegan { get; set; }
    }
}
