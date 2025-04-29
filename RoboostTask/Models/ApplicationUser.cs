using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "User first name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 20 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "User last name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 20 characters.")]
        public string LastName { get; set; }
    }
}
