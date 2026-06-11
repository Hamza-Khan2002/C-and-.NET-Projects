using AutoMapper;
using FinanceProject.DTO.Comment;
using FinanceProject.Models;

namespace FinanceProject.Mapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
