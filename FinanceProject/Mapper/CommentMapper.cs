using AutoMapper;
using FinanceProject.DTO.Comment;
using FinanceProject.Models;

namespace FinanceProject.Mapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(ap => ap.CreatedBy,
                opt => opt.MapFrom(src => src.AppUser!.UserName));
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
