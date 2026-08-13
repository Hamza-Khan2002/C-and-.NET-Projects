using FinanceProject.DTO.Stock;
using FinanceProject.Interfaces;
using FinanceProject.Mapper;
using FinanceProject.Models;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinanceProject.Services
{
    public class FMPService(HttpClient client, IConfiguration config) : IFMPService
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _config = config;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Stock?> GetStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _client.GetAsync($"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={_config["FMP_API_KEY"]}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonSerializer.Deserialize<FMPStockDto[]>(content);
                    var stock = tasks?.FirstOrDefault();

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

        public async Task<Stock?> GetStockByCompanyNameAsync(string companyName)
        {
            try
            {
                var result = await _client.GetAsync($"https://financialmodelingprep.com/stable/search-name?query={Uri.EscapeDataString(companyName)}&apikey={_config["FMP_API_KEY"]}");

                if (!result.IsSuccessStatusCode) return null;

                var content = await result.Content.ReadAsStringAsync();
                var matches = JsonSerializer.Deserialize<FMPSearchDto[]>(content, _jsonOptions);

                if (matches == null || matches.Length == 0) return null;

                // Skip ETFs / leveraged products — we only want the actual company stock.
                var candidates = matches
                    .Where(m => !string.IsNullOrWhiteSpace(m.symbol))
                    .Where(m => !m.name.Contains("ETF", StringComparison.OrdinalIgnoreCase)
                             && !m.name.Contains("Shares", StringComparison.OrdinalIgnoreCase)
                             && !m.name.Contains("Leverage", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (candidates.Count == 0) return null;

                // Prefer the primary US listing (NASDAQ/NYSE) over foreign duplicates —
                // that's the one FMP's free-tier profile endpoint actually has data for.
                var best = candidates.FirstOrDefault(m =>
                    m.exchange.Equals("NASDAQ", StringComparison.OrdinalIgnoreCase) ||
                    m.exchange.Equals("NYSE", StringComparison.OrdinalIgnoreCase))
                    ?? candidates.First();

                return await GetStockBySymbolAsync(best.symbol);
            }
            catch (Exception e)
            {
                throw new Exception($"Error fetching stock data for company {companyName}: {e.Message}", e);
            }
        }
    }
}
