using Microsoft.AspNetCore.Mvc;
using DoitFinal.Forum.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentsController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get()
    {
        var commentDTOs = await _commentService.GetAllCommentsAsync();
        return Ok(CreateApiResponse(commentDTOs, 200, true, "Comments fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> Get(int id)
    {
        try
        {
            var commentDTO = await _commentService.GetCommentByIdAsync(id);
            return Ok(CreateApiResponse(commentDTO, 200, true, "Comment fetched successfully"));
        }
        catch (Exception ex)
        {
            return NotFound(CreateApiResponse(null, 404, false, ex.Message));
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> Post([FromBody] CommentDTO commentDTO)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var createdCommentDTO = await _commentService.CreateCommentAsync(commentDTO, userId);
        return CreatedAtAction(nameof(Get), CreateApiResponse(createdCommentDTO, 201, true, "Comment created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] CommentDTO commentDTO)
    {
        try
        {
            await _commentService.UpdateCommentAsync(commentDTO);
            return Ok(CreateApiResponse(commentDTO, 200, true, "Comment updated successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(CreateApiResponse(null, 400, false, ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
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
