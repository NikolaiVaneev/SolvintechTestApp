using System.ComponentModel.DataAnnotations;

namespace SolvintechTestApp.Models
{
    public class HomeViewModel
    {
        // Для регистрации.
        [Required]
        [Display(Name = "Email")]
        public string RegistrationEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegistrationPassword { get; set; }

        [Required]
        [Compare("RegistrationPassword", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }


        // Для входа.
        [Required]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}
