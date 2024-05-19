using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ForumUser> _userManager;

        public UserRepository(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ForumUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ForumUser> GetUserByIdAsync(string emailId)
        {
            return await _userManager.FindByEmailAsync(emailId);
        }
    }
}
