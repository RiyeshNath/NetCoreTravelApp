using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookKeep.Models
{
    public class TravelLocation
    {   
        [Key]
        public int LocationId { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }

        public ICollection<UserTrip> UserTrip { get; set; }
    }
}
