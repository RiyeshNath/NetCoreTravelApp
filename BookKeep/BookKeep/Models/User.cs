using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookKeep.Models
{
    public class User: IdentityUser
    {
 
        public int Age { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        [ForeignKey("UserLocation")]
        public int LocationId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
       

        public UserLocation UserLocation { get; set; }
        public ICollection<Interest> Interests { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; }
        public ICollection<TripPlan> TripPlans { get; set; }
    }
}
