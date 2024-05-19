using DoitFinal.Forum.Models;

namespace DoitFinal.Forum.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<IEnumerable<Comment>> GetCommentsByTopicIdAsync(int topicId);
    }
}
