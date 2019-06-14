using System;
using System.ComponentModel.DataAnnotations;
using BookKeep.Models;
using System.Collections.Generic;
namespace BookKeep.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email_id { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile_number { get; set; }

        [Required]
        [Range(10, 100, ErrorMessage = "Please enter valid age between 10 and 100")]
        public int Age { get; set; }

        [Required, DataType(DataType.Text), MaxLength(256), Display(Name = "First Name")]
        public string First_name { get; set; }

        [Required, DataType(DataType.Text), MaxLength(256), Display(Name = "Last Name")]
        public string Last_name { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), MaxLength(20), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), MaxLength(20), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string Confirmpassword { get; set; }

        public IList<State> States { get; set; }
        [Required]
        public int SelectedState { get; set; }

        [Required, DataType(DataType.Text), MaxLength(50), Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip is Required"), Display(Name = "ZipCode")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip")]
        public string ZipCode { get; set; }

        public IEnumerable<int> InterestRank { get; set; }

        [Required]
        public int NightVentures { get; set; }

        [Required]
        public int FoodVentures { get; set; }

        [Required]
        public int ArtandCultureVentures { get; set; }

        [Required]
        public int OutDoors { get; set; }
    }
}
