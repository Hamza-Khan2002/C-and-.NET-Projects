using AutoMapper;
using GamesList.Data;
using GamesList.DTO;
using GamesList.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesList.Services.GameService
{
    public class GameService(AppDbContext context, IMapper mapper) : IGameServices
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<GamesListDto> CreateGameAsync(CreateGamesDto game)
        {
            var data = _mapper.Map<Games>(game);

            var genres = await _context.Genres
                .Where(g => game.GenreIds.Contains(g.Id))
                .ToListAsync();

            data.Genre = genres;

            await _context.Games.AddAsync(data);
            await _context.SaveChangesAsync();

            return _mapper.Map<GamesListDto>(data);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var data = await _context.Games.FindAsync(id);

            if (data is null) return false;

            _context.Games.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GamesListDto>> GetAllGamesAsync()
        {
            var data = await _context.Games
                .AsNoTracking()
                .Include(g => g.Genre)
                .ToListAsync();

            return _mapper.Map<List<GamesListDto>>(data);
        }

        public async Task<GamesListDto> GetGamesByIdAsync(int id)
        {
            var data = await _context.Games
                .AsNoTracking()
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (data is null) throw new KeyNotFoundException($"Check you Id again! ID #{id}");

            return _mapper.Map<GamesListDto>(data);
        }

        public async Task<bool> UpdateGameAsync(int id, UpdateGamesDto game)
        {
            var data = await _context.Games
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (data is null) return false;

            _mapper.Map(game, data);

            if (game.GenreIds != null)
            {
                var genres = await _context.Genres
                    .Where(g => game.GenreIds.Contains(g.Id))
                    .ToListAsync();

                data.Genre = genres;
            }
    
            _context.Games.Update(data);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
