using Microsoft.AspNetCore.Identity;

namespace DoitFinal.Forum.Models.Entities
{
    public class ForumUser : IdentityUser
    {
        public bool IsBanned { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
