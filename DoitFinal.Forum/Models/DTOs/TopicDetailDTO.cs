public class TopicDetailDTO : TopicDTO
{
    public ICollection<CommentDTO> Comments { get; set; }
}
