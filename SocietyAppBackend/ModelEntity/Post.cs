using System.ComponentModel.DataAnnotations;

namespace SocietyAppBackend.ModelEntity
{
    public class Post
    {
        [Key]
        public int PostId {  get; set; }
        public int UserId {  get; set; }
        public string ImageUrl {  get; set; }
        public string Caption {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; }
        public ICollection<Comment>Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
