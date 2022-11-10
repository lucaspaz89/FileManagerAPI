using System.ComponentModel.DataAnnotations;

namespace FileManagerAPI.Models
{
    public class UserLogin
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserPassword
        {
            get; set;
        }
    }
}
