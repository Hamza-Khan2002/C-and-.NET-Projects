using FinanceProject.DTO.Comment;
using FinanceProject.Helper;

namespace FinanceProject.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAllCommentAsync(CommentQueryObject queryObject);
        Task<CommentDto> GetCommentByIdAsync(int id);
        Task<CommentDto> CreateCommentAsync(string symbol, CreateCommentDto data, string username);
        Task<CommentDto> UpdateCommentAsync(int id, UpdateCommentDto data);
        Task DeleteCommentAsync(int id);
    }
}
