using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SocietyAppBackend.ModelEntity
{
    public class User
    {
        [Key]

        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string PasswordHash { get; set; }

        public string ProfilePictureUrl { get; set; } = "asdfghjkl";

        public string Bio { get; set; } = "KL 10 BOY";

        public bool IsStatus { get; set; } = true;
        public string Role { get; set; } = "User";




    }
    public class UserDto
    {
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Password { get; set; }

    }
}
