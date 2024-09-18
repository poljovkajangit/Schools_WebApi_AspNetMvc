using System.ComponentModel.DataAnnotations;

namespace Schools.MVC.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
