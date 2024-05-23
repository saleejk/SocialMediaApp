namespace SocietyAppBackend.ModelEntity
{
    public class Comment
    {
        public int CommentId {  get; set; }
        public int PostId {  get; set; }
        public int UserId {  get; set; }
        public string Text {  get; set; }
        public DateTime CreatedAt { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }


    }
}
