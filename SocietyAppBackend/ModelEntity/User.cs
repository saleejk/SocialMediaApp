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
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like>Likes { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followings{ get; set; }
    }
    public class UserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }

    }
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
