using System;
using System.Collections.Generic;
using BookKeep.Models;

namespace BookKeep.ViewModels
{
    public class TripViewModel
    {
        public UserTrip CurrTrip { get; set; }
        public ICollection<UserTrip> Trips { get; set; }
        public ICollection<TripPlan> Plans { get; set; }
    }
}
