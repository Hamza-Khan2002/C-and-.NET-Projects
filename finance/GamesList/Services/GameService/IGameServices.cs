using GamesList.DTO;

namespace GamesList.Services.GameService
{
    public interface IGameServices
    {
        Task<List<GamesListDto>> GetAllGamesAsync();
        Task<GamesListDto> GetGamesByIdAsync(int id);
        Task<GamesListDto> CreateGameAsync(CreateGamesDto game);
        Task<bool> UpdateGameAsync(int id, UpdateGamesDto game);
        Task<bool> DeleteGameAsync(int id);
    }
}
