using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Comment;
using FinanceProject.DTO.Stock;
using FinanceProject.Helper;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FinanceProject.Repositories
{
    public class CommentRepository(UserManager<AppUser> userManager, AppDbContext context, IMapper mapper, IFMPService fmpService) : ICommentRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IFMPService _fmpService = fmpService;

        public async Task<List<CommentDto>> GetAllCommentAsync(CommentQueryObject queryObject)
        {
            var comment = _context.Comments
                .Include(i => i.AppUser)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol)) comment = comment.Where(s => s.Stock!.Symbol == queryObject.Symbol);
            if (queryObject.IsDecending) comment = comment.OrderByDescending(c => c.CreatedOn);

            var commentList = await comment.ToListAsync();
            return _mapper.Map<List<CommentDto>>(commentList);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
                .Include(i => i.AppUser)
                .FirstOrDefaultAsync(i => i.Id == id);
            return comment != null ? _mapper.Map<CommentDto>(comment) : throw new KeyNotFoundException("Comment not found");
        }

        public async Task<CommentDto> CreateCommentAsync(string symbol, CreateCommentDto data, string username)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);

            if (stock == null)
            {
                stock = await _fmpService.GetStockBySymbolAsync(symbol);
                if(stock == null) throw new KeyNotFoundException("Stock not found");

                await _context.Stocks.AddAsync(stock);
                await _context.SaveChangesAsync();
            };

            var appUser = await _userManager.FindByNameAsync(username);
            var comment = _mapper.Map<Comment>(data);
            comment.StockId = stock.Id;
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
