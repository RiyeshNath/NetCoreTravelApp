using System;
using System.ComponentModel.DataAnnotations;

namespace BookKeep.ViewModels
{
    public class LogInViewModel
    {
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email_id { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), MaxLength(20), Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
        public String FacebookProvider { get; set; }
    }
}
