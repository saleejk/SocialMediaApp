namespace SocietyAppBackend.ModelEntity.Dto
{
    public class FollowDto
    {
        public int? FollowId { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
