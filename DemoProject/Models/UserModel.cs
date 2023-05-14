using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailable", "User", ErrorMessage = "Email address already in use.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
