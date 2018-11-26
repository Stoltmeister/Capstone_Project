using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class VeganSubstitute
    {
        [Key]
        public string Id { get; set; }
        public string NonVeganIngredient { get; set; }
        public string VeganSubsDescription { get; set; }

        //for(int i=0; i < AllVeganSubstitutions.foodsToSub.Count; i++)
        //{
        //    VeganSubstitute newSub = new VeganSubstitute();
        //    newSub.NonVeganIngredient = AllVeganSubstitutions.foodsToSub[i];
        //    newSub.VeganSubsDescription = AllVeganSubstitutions.veganSubs[i];
        //    await _context.VeganSubstitutes.AddAsync(newSub);
        //}
        //_context.SaveChanges();
    }
}
