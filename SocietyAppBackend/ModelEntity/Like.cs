using System.ComponentModel.DataAnnotations;

namespace SocietyAppBackend.ModelEntity
{
    public class Like
    {

        [Key]
        public int LikeId { get; set; }
        public int PostId {  get; set; }
        public int UserId {  get; set; }
        public bool IsLiked { get; set; }=false;
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
