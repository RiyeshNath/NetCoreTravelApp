using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookKeep.Models
{
    public class TripPlan
    {
        [Key]
        public int PlanId { get; set; }
        [ForeignKey("User")]
        public String UserId { get; set; }
        [ForeignKey("UserTrip")]
        public int TripId { get; set; }
        public String PlanLocation { get; set; }
        public DateTime PlanDate { get; set; }
        public double Spent { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
        public bool Status { get; set; }

        public User User { get; set; }
        public UserTrip UserTrip { get; set; }
    }
}
