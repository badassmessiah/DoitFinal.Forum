using AutoMapper;
using DoitFinal.Forum.Models.Entities;
using DoitFinal.Forum.Repositories;
using DoitFinal.Forum.Repositories.Interfaces;

public class CommentService
{
    private readonly IRepository<Comment, int> _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(IRepository<Comment, int> commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<CommentDTO> CreateCommentAsync(CommentDTO commentDTO, string userId)
    {
        
        var comment = _mapper.Map<Comment>(commentDTO);

        comment.UserId = userId;

        var createdComment = await _commentRepository.CreateAsync(comment);

        return _mapper.Map<CommentDTO>(createdComment);
    }

    public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CommentDTO>>(comments);
    }

    public async Task<CommentDTO> GetCommentByIdAsync(int id)
    {
        var comment = await _commentRepository.GetOneAsync(id);
        return _mapper.Map<CommentDTO>(comment);
    }

    public async Task UpdateCommentAsync(CommentDTO commentDTO)
    {
        var comment = _mapper.Map<Comment>(commentDTO);
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}
