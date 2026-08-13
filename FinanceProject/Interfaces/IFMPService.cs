using FinanceProject.Models;

namespace FinanceProject.Interfaces
{
    public interface IFMPService
    {
        Task<Stock?> GetStockBySymbolAsync(string symbol);
        Task<Stock?> GetStockByCompanyNameAsync(string companyName);
    }
}
