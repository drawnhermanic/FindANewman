using System.ComponentModel.DataAnnotations;

namespace FindANewman.Models.Account
{
    public class LogOnViewModel
    {
        public static readonly int MaxPasswordSize = 20;

        [Required(ErrorMessage = "You must enter your email address to login to the FindANewman site")]
        [Display(Name = "Email address")]
        //TODO: Add [RegularExpression attribute and implement static Regex helper class to validate email        
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You must enter your password to login to the FindANewman site")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}