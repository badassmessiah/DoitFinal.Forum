using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class TopicRepository : IRepository<Topic, int>
    {
        private readonly ForumDbContext _context;

        public TopicRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> CreateAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetOneAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task UpdateAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }
    }
}
