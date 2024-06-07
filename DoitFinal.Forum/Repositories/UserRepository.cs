using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class UserRepository : IUserInterface<ForumUser, string>
    {
        private readonly UserManager<ForumUser> _userManager;

        public UserRepository(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ForumUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ForumUser> GetOneAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task LockoutUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
