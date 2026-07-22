using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Comment;
using FinanceProject.DTO.Stock;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinanceProject.Repositories
{
    public class PortfolioRepository(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, IFMPService fmpService) : IPortfolioRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IFMPService _fmpService = fmpService;

        public async Task<List<StockDto>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                .Where(u => u.AppUserId == user.Id)
                .Select(stock => new StockDto
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock!.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                    Comments = stock.Stock.Comments
                        .Select(c => new CommentDto
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Content = c.Content,
                            CreatedOn = c.CreatedOn,
                            CreatedBy = c.AppUser!.UserName!
                        }).ToList()
                }).ToListAsync();
        }

        public async Task<Portfolio> AddStockToPortfolio(string username, string symbol)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);

            if(stock == null)
            {
                stock = await _fmpService.GetStockBySymbolAsync(symbol);
                if(stock == null) throw new InvalidOperationException("Stock not found");

                await _context.Stocks.AddAsync(stock);
                await _context.SaveChangesAsync();
            }

            var userPortfolio = await GetUserPortfolio(appUser!);
            if (userPortfolio.Any(cn => cn.Symbol == symbol)) throw new InvalidOperationException("Stock already in portfolio");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser!.Id,
                StockId = stock!.Id
            };
            await _context.Portfolios.AddAsync(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task DeletePortfolio(AppUser user, string companyName)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.AppUserId == user.Id 
                            && p.Stock!.CompanyName.ToLower() == companyName.ToLower());
            
            if(portfolio != null)
            {
                _context.Remove(portfolio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
