using RoboostTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address! ")]
        public string Email { get; set; }

        public RolesEnum.UserRole Role { get; set; }
    }
}
