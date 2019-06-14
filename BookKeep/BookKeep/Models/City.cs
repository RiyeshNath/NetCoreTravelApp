using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookKeep.Models
{
    public class City
    {
        [StringLength(50)]
        public string CityName { get; set; }
        [Key]
        public int CityId { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        public ICollection<UserLocation> UserLocations { get; set; }
    }
}
