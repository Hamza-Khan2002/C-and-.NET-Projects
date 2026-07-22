using FinanceProject.DTO.Stock;
using FinanceProject.Models;
using System.Runtime.CompilerServices;

namespace FinanceProject.Mapper
{
    public static class FMPStockMapper
    {
        public static Stock StockFromFMP(this FMPStockDto fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };  
        }
    }
}
