namespace DoitFinal.Forum.Repositories.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(TId id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);
    }
}
