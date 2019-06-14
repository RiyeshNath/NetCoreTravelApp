using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookKeep.Models
{
    public class Interest
    {
        [Required]
        [Range(1,5)]
        public int NightVentures { get; set; }
        [Required]
        [Range(1, 5)]
        public int FoodVentures { get; set; }
        [Required]
        [Range(1, 5)]
        public int ArtandCultureVentures { get; set; }
        [Required]
        [Range(1, 5)]
        public int OutDoors { get; set; }
        [ForeignKey ("User"), Key]
        public string UserId { get; set; }
    }
}
