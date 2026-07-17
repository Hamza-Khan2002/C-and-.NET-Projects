using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Comment;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FinanceProject.Repositories
{
    public class CommentRepository(UserManager<AppUser> userManager, AppDbContext context, IMapper mapper) : ICommentRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<List<CommentDto>> GetAllCommentAsync()
        {
            var comment = await _context.Comments
                .Include(i => i.AppUser)
                .ToListAsync();
            return _mapper.Map<List<CommentDto>>(comment);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
                .Include(i => i.AppUser)
                .FirstOrDefaultAsync(i => i.Id == id);
            return comment != null ? _mapper.Map<CommentDto>(comment) : throw new KeyNotFoundException("Comment not found");
        }

        public async Task<CommentDto> CreateCommentAsync(int stockId, CreateCommentDto data, string username)
        {
            var stock = await _context.Stocks.FindAsync(stockId);

            if (stock == null) throw new KeyNotFoundException("Stock not found");

            var appUser = await _userManager.FindByNameAsync(username);
            var comment = _mapper.Map<Comment>(data);
            comment.StockId = stockId;
            comment.AppUserId = appUser!.Id;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateCommentAsync(int id, UpdateCommentDto data)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            
            _mapper.Map(data, comment);
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
