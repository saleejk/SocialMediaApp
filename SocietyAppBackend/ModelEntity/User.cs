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

        public bool IsBlocked { get; set; } = false;
        public bool? IsActive { get; set; } = false;
        public string Role { get; set; } = "User";
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like>Likes { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followings{ get; set; }
        //public ICollection<Message> SendMessege {  get; set; }
        //public ICollection<Message> RecievedMessege { get; set; }
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
      public class UpdateUserDto
    {
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Bio { get; set; } = "KL 10 BOY";

    }

    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class UserViewDto
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string PasswordHash { get; set; }

        public string ProfilePictureUrl { get; set; } = "asdfghjkl";

        public string Bio { get; set; } = "KL 10 BOY";

        public bool IsBlocked { get; set; } = false;
        public bool? IsActive { get; set; } = false;
        public string Role { get; set; } = "User";
    }

    public class UserViewByIdFollow
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
      

        public string PasswordHash { get; set; }

        public string ProfilePictureUrl { get; set; } 

        public string Bio { get; set; } 

        public bool IsBlocked { get; set; } 
        public bool? IsActive { get; set; }
        public string Role { get; set; } 
        public int Posts { get; set; }
        public int Followers { get; set; }
        public int Followings { get; set; }
    }
}
