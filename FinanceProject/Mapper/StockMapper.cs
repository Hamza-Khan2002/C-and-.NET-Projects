using AutoMapper;
using FinanceProject.DTO.Stock;
using FinanceProject.Models;

namespace FinanceProject.Mapper
{
    public class StockMapper : Profile
    {
        public StockMapper() {
            
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();
        }


    }
}
