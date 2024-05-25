namespace SocietyAppBackend.ModelEntity.Dto
{
    public class PostDto
    {
        public string Caption { get; set; }
    }
    public class PostViewDto {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


}
