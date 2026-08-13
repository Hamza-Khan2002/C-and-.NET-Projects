using AutoMapper;
using BanquetManagementSystem.DTOs;
using BanquetManagementSystem.Interface;
using BanquetManagementSystem.Models;

namespace BanquetManagementSystem.Mapper
{
    public class BanquetBookingMapper : Profile
    {
        public BanquetBookingMapper()
        {
            CreateMap<BanquetBooking, BanquetBookingDto>()
                .ForMember(p => p.PackageName, 
                option => option.MapFrom(src => src.BanquetPackage!.Title));

            CreateMap<CreateBanquetBookingDto, BanquetBooking>();
        }
    }
}
