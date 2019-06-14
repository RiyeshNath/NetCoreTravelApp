using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookKeep.Models
{
    public class UserLocation
    {
        [Key]
        public int LocationId { get; set; }
        public int ZipCode { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }
        public ICollection<User>User { get; set; }
    }
}
