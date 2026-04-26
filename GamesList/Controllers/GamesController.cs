using GamesList.Data;
using GamesList.DTO;
using GamesList.Models;
using GamesList.Services.GameService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesList.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController(IGameServices services) : ControllerBase
    {
        private readonly IGameServices _services = services;

        [HttpGet]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            var data = await _services.GetAllGamesAsync();

            return data.Any() ? Ok(data) : BadRequest();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGameByIdAsync([FromRoute] int id)
        {
            try
            {
                var data = await _services.GetGamesByIdAsync(id);
                return data is null ? NotFound() : Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"{ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameAsync([FromBody] CreateGamesDto game)
        {
            var data = await _services.CreateGameAsync(game);

            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGameAsync([FromRoute]int id)
        {
            var data = await _services.DeleteGameAsync(id);
            return !data ? NotFound(new {message = $"Check you Id again! ID #{id}" }) : NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGameAsync([FromRoute] int id, [FromBody] UpdateGamesDto game)
        {
            var data = await _services.UpdateGameAsync(id, game);
            return !data ? NotFound(new {message = $"Check your Id again! ID#{id}"}) : NoContent();
        }

    }
    
}
