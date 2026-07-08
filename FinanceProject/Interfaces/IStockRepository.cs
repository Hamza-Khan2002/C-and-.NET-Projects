using FinanceProject.DTO.Stock;
using FinanceProject.Helper;

namespace FinanceProject.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetStocksAsync(QueryObject query);
        Task<StockDto> GetStockByIdAsync(int id);
        Task<StockDto> GetStockByCompanyNameAsync(string CompanyName);
        Task<StockDto> CreateStockAsync(CreateStockDto data);
        Task<StockDto> UpdateStockAsync(int id, UpdateStockDto data);
        Task DeleteStockAsync(int id);
    }
}
