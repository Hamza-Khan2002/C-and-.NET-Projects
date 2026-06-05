using FinanceProject.DTO.Stock;

namespace FinanceProject.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetStocksAsync();
        Task<StockDto> GetStockByIdAsync(int id);
        Task<StockDto> CreateStockAsync(CreateStockDto data);
        Task<StockDto> UpdateStockAsync(int id, UpdateStockDto data);
        Task DeleteStockAsync(int id);
    }
}
