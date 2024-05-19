namespace DoitFinal.Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public string UserId { get; set; }
        public ForumUser User { get; set; }
    }
}
