using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignUp___SignIn_Form.Data;
using SignUp___SignIn_Form.Interfaces;
using SignUp___SignIn_Form.Models;

namespace SignUp___SignIn_Form.Repository
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            return await _context.Users.
                AsNoTracking().
                FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistance(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

    }
}
