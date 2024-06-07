using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories;
using DoitFinal.Forum.Repositories.Interfaces;

public class TopicService
{
    private readonly IRepository<Topic, int> _topicRepository;

    public TopicService(IRepository<Topic, int> topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<Topic> CreateTopicAsync(Topic topic)
    {
        return await _topicRepository.CreateAsync(topic);
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
    {
        return await _topicRepository.GetAllAsync();
    }

    public async Task<Topic> GetTopicByIdAsync(int id)
    {
        return await _topicRepository.GetOneAsync(id);
    }

    public async Task UpdateTopicAsync(Topic topic)
    {
        await _topicRepository.UpdateAsync(topic);
    }

    public async Task DeleteTopicAsync(int id)
    {
        await _topicRepository.DeleteAsync(id);
    }
}
