using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Stock;
using FinanceProject.Helper;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Repositories
{
    public class StockRepository(AppDbContext context, IMapper mapper):IStockRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<StockDto>> GetStocksAsync(QueryObject query)
        {
            var stock = _context.Stocks
                .Include(c => c.Comments)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Symbol)) stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            if(!string.IsNullOrWhiteSpace(query.CompanyName)) stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                stock = query.SortBy.ToLower() switch
                {
                    "symbol" => query.IsDescending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol),
                    "purchase" => query.IsDescending ? stock.OrderByDescending(s => s.Purchase) : stock.OrderBy(s => s.Purchase),
                    "lastdiv" => query.IsDescending ? stock.OrderByDescending(s => s.LastDiv) : stock.OrderBy(s => s.LastDiv),
                    "marketcap" => query.IsDescending ? stock.OrderByDescending(s => s.MarketCap) : stock.OrderBy(s => s.MarketCap),
                    _ => stock.OrderBy(s => s.Purchase)
                };
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
 
            return _mapper.Map<List<StockDto>>(await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync());
        }

        public async Task<StockDto> GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);
            return stock == null ? throw new KeyNotFoundException("Stock not found") : _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> CreateStockAsync(CreateStockDto data)
        {
            var stock = _mapper.Map<Stock>(data);
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> UpdateStockAsync(int id, UpdateStockDto data)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) throw new KeyNotFoundException("Stock not found");

            _mapper.Map(data, stock);
            await _context.SaveChangesAsync();
            return _mapper.Map<StockDto>(stock);
        }

        public async Task DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) throw new KeyNotFoundException("Stock not found");

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }

    }
}
