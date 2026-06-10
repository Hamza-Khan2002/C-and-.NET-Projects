using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Stock;
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

        public async Task<List<StockDto>> GetStocksAsync()
        {
            var stock = await _context.Stocks
                .Include(c => c.Comments)
                .ToListAsync();
            return _mapper.Map<List<StockDto>>(stock);
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
