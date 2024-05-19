using DoitFinal.Forum.Models;

namespace DoitFinal.Forum.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> CreateTopicAsync(Topic topic);
        Task<Topic> GetTopicByIdAsync(int id);
        Task<IEnumerable<Topic>> GetAllTopicsAsync();

        Task<Topic> UpdateTopic(Topic topic);
        void DeleteTopicAsync(int id);

    }
}
