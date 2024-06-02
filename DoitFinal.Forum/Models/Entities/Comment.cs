using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DoitFinal.Forum.Models.Entities
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey(nameof(Entities.Topic))]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [Required]
        [ForeignKey(nameof(ForumUser))]
        public string UserId { get; set; }
        public ForumUser User { get; set; }
    }
}
