using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoitFinal.Forum.Models.Entities
{
    public enum TopicState
    {
        Pending,
        Show,
        Hide
    }

    public enum TopicStatus
    {
        Active,
        Inactive
    }

    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public TopicState State { get; set; }

        [Required]
        public TopicStatus Status { get; set; }

        [Required]
        [ForeignKey(nameof(ForumUser))]
        public string UserId { get; set; }
        public ForumUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
