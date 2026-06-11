using AutoMapper;
using FinanceProject.Data;
using FinanceProject.DTO.Stock;
using FinanceProject.Helper;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(IStockRepository repository) : ControllerBase
    {
        private readonly IStockRepository _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetStocksAsync([FromQuery]QueryObject query)
        {
            try
            {
                return Ok(await _repository.GetStocksAsync(query));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        [ActionName("GetStockById")]
        public async Task<IActionResult> GetStockByIdAsync([FromRoute]int id)
        {
            try
            {
                return Ok(await _repository.GetStockByIdAsync(id));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockAsync([FromBody] CreateStockDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _repository.CreateStockAsync(data);
            return CreatedAtAction("GetStockById", new { id = stock.Id }, stock);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStockAsync([FromRoute] int id, [FromBody] UpdateStockDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return Ok(await _repository.UpdateStockAsync(id, data));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStockAsync([FromRoute] int id)
        {
            try
            {
               await _repository.DeleteStockAsync(id);
               return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
