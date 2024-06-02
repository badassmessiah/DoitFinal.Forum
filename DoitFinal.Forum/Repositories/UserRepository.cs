using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class UserRepository : IRepository<ForumUser>
    {
        private readonly UserManager<ForumUser> _userManager;

        public UserRepository(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ForumUser> CreateAsync(ForumUser user)
        {
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new Exception("Failed to create user");
            }
        }

        public async Task<IEnumerable<ForumUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ForumUser> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task UpdateAsync(ForumUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
