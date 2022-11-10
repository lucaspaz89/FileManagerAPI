using System.ComponentModel.DataAnnotations;

namespace FileManagerAPI.Models
{
    public class UserRegistration
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [Required]
        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
    }
}
