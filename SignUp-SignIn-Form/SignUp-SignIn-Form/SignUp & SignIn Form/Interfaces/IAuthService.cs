using SignUp___SignIn_Form.DTOs.Request;
using SignUp___SignIn_Form.DTOs.Response;

namespace SignUp___SignIn_Form.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<UserResponse> GetUserByIdAsync(int id);
    }
}
