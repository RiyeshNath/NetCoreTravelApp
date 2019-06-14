using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookKeep.Models
{
    public class State
    {
        [StringLength(50)]
        public string StateName { get; set; }
        [Key]
        public int StateId { get; set; }
       // public SelectList StateList { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
