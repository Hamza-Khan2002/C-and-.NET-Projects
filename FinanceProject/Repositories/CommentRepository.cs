using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Comment;
using FinanceProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Repositories
{
    public class CommentRepository(AppDbContext context, IMapper mapper) : ICommentRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<CommentDto>> GetAllCommentAsync()
        {
            var comment = await _context.Comments
                .Include(st => st.Stock)
                .ToListAsync();
            return _mapper.Map<List<CommentDto>>(comment);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
                .Include(st => st.Stock)
                .FirstOrDefaultAsync(i => i.Id == id);
            return comment != null ? _mapper.Map<CommentDto>(comment) : throw new KeyNotFoundException("Comment not found");
        }
    }
}
