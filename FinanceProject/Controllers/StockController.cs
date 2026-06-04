using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Stock;
using FinanceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetStocksAsync()
        {
            var stock = await _context.Stocks.ToListAsync();
            return Ok(_mapper.Map<List<StockDto>>(stock));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock == null ? NotFound() : Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockAsync([FromBody] CreateStockDto data)
        {
            var stock = _mapper.Map<Stock>(data);
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockByIdAsync), new {id = stock.Id}, _mapper.Map<StockDto>(stock));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStockAsync([FromRoute] int id, [FromBody] UpdateStockDto data)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) return NotFound();

            _mapper.Map(data, stock);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStockAsync([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return NotFound();

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
