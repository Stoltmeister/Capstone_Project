using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class UserEatery
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("StandardUser")]
        public string StandardUserId { get; set; }
        public StandardUser StandardUser { get; set; }
        public string Name { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public bool HasVeganOptions { get; set; }
    }
}
