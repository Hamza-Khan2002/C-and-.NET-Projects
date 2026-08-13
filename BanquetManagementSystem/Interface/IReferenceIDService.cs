using BanquetManagementSystem.DTOs;

namespace BanquetManagementSystem.Interface
{
    public interface IReferenceIDService
    {
        string GenerateReferenceId(DateOnly bookingDate);
    }
}
