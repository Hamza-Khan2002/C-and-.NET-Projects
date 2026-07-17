using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Stock;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Repositories
{
    public class PortfolioRepository(AppDbContext context, IMapper mapper) : IPortfolioRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

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
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }

        public async Task<Portfolio> AddStockToPortfolio(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
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
