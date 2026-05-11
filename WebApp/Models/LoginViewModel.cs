using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Max 50 or min 5 characters allowed")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string Password { get; set; }
    }
}
