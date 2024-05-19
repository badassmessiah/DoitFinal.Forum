using DoitFinal.Forum.Models;

namespace DoitFinal.Forum.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ForumUser> GetUserByIdAsync(string emailId);
        Task<IEnumerable<ForumUser>> GetAllUsersAsync();
    }
}
