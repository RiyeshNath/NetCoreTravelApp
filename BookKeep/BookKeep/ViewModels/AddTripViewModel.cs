using System;
using System.ComponentModel.DataAnnotations;

namespace BookKeep.ViewModels
{
    public class AddTripViewModel
    {
        [Required, DataType(DataType.Text), MaxLength(256), Display(Name = "Trip Name")]
        public String TripName { get; set; }

        [Required, DataType(DataType.Text), MaxLength(256), Display(Name = "Country")]
        public String Country { get; set; }

        [Required, DataType(DataType.Text), MaxLength(256), Display(Name = "City")]
        public String City { get; set; }

        [Required, Range(100, 100000, ErrorMessage = "Please enter a Budget Between 100 and 100,000")]
        public double Budget { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
