using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
