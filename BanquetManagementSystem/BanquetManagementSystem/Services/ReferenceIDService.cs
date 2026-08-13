using BanquetManagementSystem.Interface;

namespace BanquetManagementSystem.Services
{
    public class ReferenceIDService : IReferenceIDService
    {
        public string GenerateReferenceId(DateOnly bookingDate)
        {
            string datePart = bookingDate.ToString("yyyyMMdd");
            string randomPart = new Random().Next(1000, 9999).ToString().ToUpper();
            return $"NOOR-{datePart}-{randomPart}";
        }
    }
}
