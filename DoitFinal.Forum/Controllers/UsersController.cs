using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TopicService _topicService;
    private readonly CommentService _commentService;

    public UsersController(UserService userService, TopicService topicService, CommentService commentService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
        _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }

    [HttpGet("topic/{id}")]
    public async Task<ActionResult<ApiResponse>> GetUserByTopicId(int id)
    {

        var topic = await _topicService.GetTopicByIdAsync(id);
        if (topic == null)
        {
            return NotFound(CreateApiResponse(null, 404, false, "Topic not found"));
        }

        var user = await _userService.GetUserByEmailAsync(topic.UserEmail);
        return Ok(CreateApiResponse(user, 200, true, "User retrieved successfully"));

    }

    [HttpGet("comment/{Commentid}")]
    public async Task<ActionResult<ApiResponse>> GetUserByCommentId(int Commentid)
    {
        var comment = await _commentService.GetCommentByIdAsync(Commentid);
        if (comment == null)
        {
            return NotFound(CreateApiResponse(null, 404, false, "Comment not found"));
        }

        var user = await _userService.GetUserByEmailAsync(comment.UserEmail);
        return Ok(CreateApiResponse(user, 200, true, "User retrieved successfully"));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(CreateApiResponse(users, 200, true, "Users retrieved successfully"));
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<ApiResponse>> GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(CreateApiResponse(null, 400, false, "Email cannot be null or empty"));
        }

        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound(CreateApiResponse(null, 404, false, "User not found"));
        }

        return Ok(CreateApiResponse(user, 200, true, "User retrieved successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("email/{email}/lockout")]
    public async Task<ActionResult<ApiResponse>> LockOutUser(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(CreateApiResponse(null, 400, false, "Email cannot be null or empty"));
        }

        await _userService.LockOutUserAsync(email);
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
