using AutoMapper;
using DoitFinal.Forum.Models.Entities;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Topic, TopicDTO>();
        CreateMap<TopicDTO, Topic>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Topic, TopicDetailDTO>();
        CreateMap<Comment, CommentDTO>();
        CreateMap<CommentDTO, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
