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
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get()
    {
        var topicDTOs = await _topicService.GetAllTopicsAsync();
        return Ok(CreateApiResponse(topicDTOs, 200, true, "Topics fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> Get(int id)
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
    public async Task<ActionResult<ApiResponse>> Post([FromBody] TopicDTO topicDTO)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        string userEmail = User.Identity.Name;
        string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var createdTopicDTO = await _topicService.CreateTopicAsync(topicDTO, userEmail, UserId);
        return CreatedAtAction(nameof(Get), CreateApiResponse(createdTopicDTO, 201, true, "Topic created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] TopicDTO topicDTO)
    {
        try
        {
            await _topicService.UpdateTopicAsync(id, topicDTO);
            return Ok(CreateApiResponse(null, 200, true, "Topic updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
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
