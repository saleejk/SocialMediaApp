namespace SocietyAppBackend.ModelEntity
{
    public class Post
    {
        public int PostId {  get; set; }
        public int UserId {  get; set; }
        public string ImageUrl {  get; set; }
        public string Caption {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public User User { get; set; }
        public ICollection<Comment>Comments { get; set; }
    }
}
