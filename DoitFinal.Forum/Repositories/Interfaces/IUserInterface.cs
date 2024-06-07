using System.Security.Cryptography;

namespace DoitFinal.Forum.Repositories.Interfaces
{
    public interface IUserInterface<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(TId id);
        Task LockoutUser(TId id);
    }
}
