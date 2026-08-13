using AutoMapper;
using BanquetManagementSystem.Data;
using BanquetManagementSystem.DTOs;
using BanquetManagementSystem.Interface;
using BanquetManagementSystem.Interface.User;
using BanquetManagementSystem.Models;
using BanquetManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BanquetManagementSystem.Repository
{
    public class BanquetBookingRepository(ApplicationDbContext context, IMapper mapper, IReferenceIDService service) : IBanquetBookingRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IReferenceIDService _service = service;

        public async Task<List<BanquetBookingDto>> GetAllBooking()
        {
            var result = await _context.BanquetBookings.ToListAsync();
            return _mapper.Map<List<BanquetBookingDto>>(result);
        }

        public async Task<BanquetBookingDto> GetBookingByReferenceId(string referenceId)
        {
            var result = await _context.BanquetBookings.FirstOrDefaultAsync(r => r.ReferenceId == referenceId);
            return result != null ? _mapper.Map<BanquetBookingDto>(result) : throw new Exception("Record Not Found");
        }

        public async Task CreateBooking(CreateBanquetBookingDto data)
        {
            var booking = _mapper.Map<BanquetBooking>(data);
            booking.ReferenceId = _service.GenerateReferenceId(booking.BookingDate);

            await _context.BanquetBookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }
    }
}
