using FinanceProject.Models;
using FinanceProject.Interfaces;
using FinanceProject.Mapper;
using System.Text.Json;
using System.Text.Json.Serialization;
using FinanceProject.DTO.Stock;

namespace FinanceProject.Services
{
    public class FMPService(HttpClient client, IConfiguration config) : IFMPService
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _config = config;

        public async Task<Stock?> GetStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _client.GetAsync($"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={_config["FMP_API_KEY"]}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonSerializer.Deserialize<FMPStockDto[]>(content);
                    var stock = tasks?[0];

                    if (stock != null)
                    {
                        return stock.StockFromFMP();
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Error fetching stock data for symbol {symbol}: {e.Message}", e);
            }
        }
    }
}
