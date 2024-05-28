using System.ComponentModel.DataAnnotations;

namespace SocietyAppBackend.ModelEntity
{
    public class Follow
    {

        [Key]
        public int? FollowId { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId {  get; set; }
        public DateTime CreatedAt { get; set; }
        public User? Follower { get; set; }
        public User? Following { get; set; }
    }
}
