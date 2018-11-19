using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class UserFood
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("StandardUsers")]
        public string StandardUserId { get; set; }
        public StandardUser StandardUser { get; set; }
        [ForeignKey("Foods")]
        public string FoodId { get; set; }
        public Food Food { get; set; }
    }
}
