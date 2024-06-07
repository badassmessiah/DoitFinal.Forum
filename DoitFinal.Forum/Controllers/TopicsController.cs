using Microsoft.AspNetCore.Mvc;
using DoitFinal.Forum.Models.Entities;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly TopicService _topicService;

    public TopicsController(TopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get()
    {
        var topics = await _topicService.GetAllTopicsAsync();
        return Ok(CreateApiResponse(topics, 200, true, "Topics fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> Get(int id)
    {
        try
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            return Ok(CreateApiResponse(topic, 200, true, "Topic fetched successfully"));
        }
        catch (Exception ex)
        {
            return NotFound(CreateApiResponse(null, 404, false, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Post([FromBody] Topic topic)
    {
        var createdTopic = await _topicService.CreateTopicAsync(topic);
        return CreatedAtAction(nameof(Get), new { id = createdTopic.Id }, CreateApiResponse(createdTopic, 201, true, "Topic created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] Topic topic)
    {
        if (id != topic.Id)
        {
            return BadRequest(CreateApiResponse(null, 400, false, "ID mismatch"));
        }
        await _topicService.UpdateTopicAsync(topic);
        return Ok(CreateApiResponse(topic, 200, true, "Topic updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        await _topicService.DeleteTopicAsync(id);
        return NoContent();
    }

    private ApiResponse CreateApiResponse(object result, int statusCode, bool isSuccess, string message)
    {
        return new ApiResponse
        {
            Result = result,
            StatusCode = statusCode,
            IsSuccess = isSuccess,
            Message = message
        };
    }
}
