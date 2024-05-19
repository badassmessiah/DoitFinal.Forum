using DoitFinal.Forum.Data;
using DoitFinal.Forum.Models;
using DoitFinal.Forum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumDbContext _context;

        public TopicRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> CreateTopicAsync(Topic topic)
        {
            var newTopic = await _context.Topics.AddAsync(topic);
            return newTopic.Entity;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public Task<Topic> GetTopicByIdAsync(int id)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(topic);
        }

        public async Task<Topic> UpdateTopicAsync(Topic topic)
        {
            var updatedTopic = _context.Topics.Update(topic);
            return updatedTopic.Entity;
        }
        public void DeleteTopicAsync(int id)
        {
            _context.Topics.Remove(new Topic { Id = id });
        }
    }
}
