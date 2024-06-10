using Microsoft.AspNetCore.Mvc;
using DoitFinal.Forum.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly CommentService _commentService;
    private readonly TopicService _topicService;

    public CommentsController(CommentService commentService, TopicService topicService)
    {
        _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
    }

    [HttpGet("topic/{id}")]
    public async Task<ActionResult<ApiResponse>> GetAllCommentsByTopic(int id)
    {
        var topicComments = await _topicService.GetTopicByIdAsync(id);

        var comments = topicComments.Comments.ToList();
        return Ok(CreateApiResponse(comments, 200, true, "Comments fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetCommentById(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(CreateApiResponse(comment, 200, true, "Comment fetched successfully"));
        }
        catch (Exception ex)
        {
            return NotFound(CreateApiResponse(null, 404, false, ex.Message));
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> CreateComment([FromBody] CommentDTO commentDTO)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var topic = await _topicService.GetTopicByIdAsync(commentDTO.TopicId);
        if (topic.Status == TopicStatus.Inactive)
        {
            return BadRequest(CreateApiResponse(null, 400, false, "Topic is inactive and comments cannot be added"));
        }

        string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var createdCommentDTO = await _commentService.CreateCommentAsync(commentDTO, userId);
        return CreatedAtAction(nameof(GetCommentById), CreateApiResponse(createdCommentDTO, 201, true, "Comment created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateComment(int id, [FromBody] CommentDTO commentDTO)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            var topic = await _topicService.GetTopicByIdAsync(comment.TopicId);
            if (topic.Status == TopicStatus.Inactive)
            {
                return BadRequest(CreateApiResponse(null, 400, false, "Topic is inactive and comments cannot be updated"));
            }

            string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
            if (comment.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _commentService.UpdateCommentAsync(commentDTO);
            return Ok(CreateApiResponse(commentDTO, 200, true, "Comment updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> DeleteComment(int id)
    {

        var comment = await _commentService.GetCommentByIdAsync(id);
        string currentUserEmail = User.FindFirst(ClaimTypes.Email).Value;
        if (comment.UserEmail != currentUserEmail)
        {
            return Unauthorized();
        }

        await _commentService.DeleteCommentAsync(id);
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
