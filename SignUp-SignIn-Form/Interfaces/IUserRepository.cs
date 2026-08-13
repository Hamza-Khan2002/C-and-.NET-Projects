using SignUp___SignIn_Form.Models;

namespace SignUp___SignIn_Form.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<bool> UserExistance(string email);
    }
}
