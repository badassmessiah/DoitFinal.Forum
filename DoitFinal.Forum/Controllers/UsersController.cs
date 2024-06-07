using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(CreateApiResponse(users, 200, true, "Users retrieved successfully"));
    }

    [HttpGet("{email}")]
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

    [HttpPost("{email}/lockout")]
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
