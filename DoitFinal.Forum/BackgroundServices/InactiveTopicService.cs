using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories.Interfaces;


public class InactiveTopicService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private const int DaysUntilInactive = 7;
    DateTime inactiveThreshold = DateTime.UtcNow.AddMinutes(-1);

    public InactiveTopicService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _topicRepository = scope.ServiceProvider.GetRequiredService<IRepository<Topic, int>>();

                var topics = await _topicRepository.GetAllAsync();

                foreach (var topic in topics)
                {
                    var topicWithComments = await _topicRepository.GetOneWithCommentsAsync(topic.Id);
                    var lastComment = topicWithComments.Comments.OrderByDescending(c => c.CreatedAt).FirstOrDefault();

                    if (lastComment != null && (DateTime.Now - lastComment.CreatedAt) > inactiveThreshold.TimeOfDay)
                    {
                        topic.Status = TopicStatus.Inactive;
                        await _topicRepository.UpdateAsync(topic);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}
