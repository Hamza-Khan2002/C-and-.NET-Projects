using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Comment;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FinanceProject.Repositories
{
    public class CommentRepository(AppDbContext context, IMapper mapper) : ICommentRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<CommentDto>> GetAllCommentAsync()
        {
            var comment = await _context.Comments
                .ToListAsync();
            return _mapper.Map<List<CommentDto>>(comment);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(i => i.Id == id);
            return comment != null ? _mapper.Map<CommentDto>(comment) : throw new KeyNotFoundException("Comment not found");
        }

        public async Task<CommentDto> CreateCommentAsync(int stockId, CreateCommentDto data)
        {
            var stock = await _context.Stocks.FindAsync(stockId);

            if (stock == null) throw new KeyNotFoundException("Stock not found");

            var comment = _mapper.Map<Comment>(data);
            comment.StockId = stockId;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentDto>(comment);
        }
        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment == null) throw new KeyNotFoundException("Comment not found");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
