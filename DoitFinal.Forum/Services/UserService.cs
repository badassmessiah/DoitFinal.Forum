using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

public class UserService
{
    private readonly IUserInterface<ForumUser, string> _userRepository;

    public UserService(IUserInterface<ForumUser, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ForumUser>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<ForumUser> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetOneAsync(email);
    }

    public async Task LockOutUserAsync(string email)
    {
        await _userRepository.LockoutUser(email);
    }
}
