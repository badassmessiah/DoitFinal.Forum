using Microsoft.AspNetCore.Mvc;
using DoitFinal.Forum.Models.Entities;

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
        var comments = await _commentService.GetAllCommentsAsync();
        var commentDTOs = comments.Select(c => new CommentDTO
        {
            Id = c.Id,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            TopicId = c.TopicId,
            UserId = c.UserId
        });
        return Ok(CreateApiResponse(commentDTOs, 200, true, "Comments fetched successfully"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> Get(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            var commentDTO = new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                TopicId = comment.TopicId,
                UserId = comment.UserId
            };
            return Ok(CreateApiResponse(commentDTO, 200, true, "Comment fetched successfully"));
        }
        catch (Exception ex)
        {
            return NotFound(CreateApiResponse(null, 404, false, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Post([FromBody] CommentDTO commentDTO)
    {
        var comment = new Comment
        {
            Content = commentDTO.Content,
            TopicId = commentDTO.TopicId,
            UserId = commentDTO.UserId
        };
        var createdComment = await _commentService.CreateCommentAsync(comment);
        commentDTO.Id = createdComment.Id;
        commentDTO.CreatedAt = createdComment.CreatedAt;
        return CreatedAtAction(nameof(Get), new { id = createdComment.Id }, CreateApiResponse(commentDTO, 201, true, "Comment created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] CommentDTO commentDTO)
    {
        if (id != commentDTO.Id)
        {
            return BadRequest(CreateApiResponse(null, 400, false, "ID mismatch"));
        }
        var comment = new Comment
        {
            Id = commentDTO.Id,
            Content = commentDTO.Content,
            TopicId = commentDTO.TopicId,
            UserId = commentDTO.UserId
        };
        await _commentService.UpdateCommentAsync(comment);
        return Ok(CreateApiResponse(commentDTO, 200, true, "Comment updated successfully"));
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
