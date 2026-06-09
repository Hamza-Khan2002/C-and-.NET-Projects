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
                .ForMember(sm => sm.StockName, options => 
                options.MapFrom(src => src.Stock != null ? src.Stock.CompanyName : null));
        }
    }
}
