using BanquetManagementSystem.DTOs;

namespace BanquetManagementSystem.Interface.User
{
    public interface IBanquetBookingRepository
    {
        Task<List<BanquetBookingDto>> GetAllBooking();
        Task<BanquetBookingDto> GetBookingByReferenceId(string referenceId);
        Task CreateBooking(CreateBanquetBookingDto bookingDto);
    }
}
