using BanquetManagementSystem.DTOs;

namespace BanquetManagementSystem.Interface.User
{
    public interface IBanquetBooking
    {
        Task<List<BanquetBookingDto>> GetAllBooking();
        Task<BanquetBookingDto> GetBookingByReferenceId(string referenceId);
        Task<BanquetBookingDto> CreateBooking(CreateBanquetBookingDto bookingDto);
    }
}
