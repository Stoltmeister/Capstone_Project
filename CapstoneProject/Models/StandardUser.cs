using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class StandardUser
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }       
        [Display(Name = "First Name")]
        public string FirstName { get; set; }        
        public string Email { get; set; }       
        [Display(Name = "Zipcode")]
        public int? ZipCode { get; set; }        
        public bool IsGettingWeeklyEmail { get; set; }
    }
}
