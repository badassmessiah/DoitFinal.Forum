using DoitFinal.Forum.Models.Entities;

public class TopicDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public TopicState State { get; set; }
    public TopicStatus Status { get; set; }
    public string UserId { get; set; }
}
