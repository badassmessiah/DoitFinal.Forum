using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories;
using DoitFinal.Forum.Repositories.Interfaces;

public class CommentService
{
    private readonly IRepository<Comment, int> _commentRepository;

    public CommentService(IRepository<Comment, int> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        return await _commentRepository.CreateAsync(comment);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        return await _commentRepository.GetAllAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetOneAsync(id);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}
