public class CommentDTO
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TopicId { get; set; }
    public string UserEmail { get; set; } // Change UserId to UserEmail
}
