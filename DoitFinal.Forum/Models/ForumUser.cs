using Microsoft.AspNetCore.Identity;

namespace DoitFinal.Forum.Models
{
    public class ForumUser : IdentityUser
    {
        public bool IsBanned { get; set; }
    }
}
