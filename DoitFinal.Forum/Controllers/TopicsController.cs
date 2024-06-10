using Microsoft.AspNetCore.Mvc;
using DoitFinal.Forum.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly TopicService _topicService;

    public TopicsController(TopicService topicService)
    {
        _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAllTopics()
    {
        var topicDTOs = await _topicService.GetAllTopicsAsync();
        return Ok(CreateApiResponse(topicDTOs, 200, true, "Topics fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetTopicById(int id)
    {
        try
        {
            var topicDetailDTO = await _topicService.GetTopicByIdAsync(id);
            return Ok(CreateApiResponse(topicDetailDTO, 200, true, "Topic with comments fetched successfully"));
        }
        catch (Exception ex)
        {
            return NotFound(CreateApiResponse(null, 404, false, ex.Message));
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> CreateTopic([FromBody] TopicDTO topicDTO)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        string userEmail = User.Identity.Name;
        string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var createdTopicDTO = await _topicService.CreateTopicAsync(topicDTO, userEmail, UserId);
        return CreatedAtAction(nameof(GetTopicById), CreateApiResponse(createdTopicDTO, 201, true, "Topic created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateTopic(int id, [FromBody] TopicDTO topicDTO)
    {
        try
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic.Status == TopicStatus.Inactive)
            {
                return BadRequest(CreateApiResponse(null, 400, false, "Topic is inactive and cannot be updated"));
            }
            string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
            if (topic.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _topicService.UpdateTopicAsync(id, topicDTO);
            return Ok(CreateApiResponse(null, 200, true, "Topic updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
    }

    [HttpPatch("{id}/state")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateTopicState(int id, [FromBody] TopicState state)
    {
        try
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
            if (topic.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _topicService.UpdateTopicStateAsync(id, state);
            return Ok(CreateApiResponse(null, 200, true, "Topic state updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
    }

    [HttpPatch("{TopicId}/status")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateTopicStatus(int TopicId, [FromBody] TopicStatus status)
    {
        try
        {
            var topic = await _topicService.GetTopicByIdAsync(TopicId);
            string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
            if (topic.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _topicService.UpdateTopicStatus(TopicId, status);
            return Ok(CreateApiResponse(null, 200, true, "Topic status updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> DeleteTopic(int id)
    {
        var topic = await _topicService.GetTopicByIdAsync(id);
        string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
        if (topic.UserEmail != currentUserEmail)
        {
            return Unauthorized();
        }

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
