namespace DoitFinal.Forum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string State { get; set; } // Pending, Show, Hide
        public string Status { get; set; } // Active, Inactive
        public string UserId { get; set; }
        public ForumUser User { get; set; }
    }
}
