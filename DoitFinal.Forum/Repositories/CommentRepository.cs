using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumDbContext _context;

        public CommentRepository(ForumDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            await _context.Comments.ToListAsync();
            return _context.Comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTopicIdAsync(int topicId)
        {
            await _context.Comments.Where(c => c.TopicId == topicId).ToListAsync();
            return _context.Comments.Where(c => c.TopicId == topicId);
        }
    }
}
