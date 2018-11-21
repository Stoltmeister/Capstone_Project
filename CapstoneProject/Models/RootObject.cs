using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class RootObject
    {
        public List<Match> matches { get; set; }
        public Attribution attribution { get; set; }
        public List<LocationSuggestion> location_suggestions { get; set; }
    }
}