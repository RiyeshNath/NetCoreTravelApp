using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookKeep.Models
{
    public class UserTrip
    {
        [Key]
        public int TripId { get; set; }
        [ForeignKey("TravelLocation")]
        public int LocationId { get; set; }
        [ForeignKey("User")]
        public String UserId { get; set; }
        public String TripName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Budget { get; set; }
        public double Spent { get; set; }

        public User User { get; set; }
        public TravelLocation TravelLocation { get; set; }
        public ICollection<TripPlan> TripPlans { get; set; }
    }
}
