using FinanceProject.DTO.Comment;

namespace FinanceProject.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAllCommentAsync();
        Task<CommentDto> GetCommentByIdAsync(int id);
        Task<CommentDto> CreateCommentAsync(int stockId, CreateCommentDto data, string username);
        Task<CommentDto> UpdateCommentAsync(int id, UpdateCommentDto data);
        Task DeleteCommentAsync(int id);
    }
}
