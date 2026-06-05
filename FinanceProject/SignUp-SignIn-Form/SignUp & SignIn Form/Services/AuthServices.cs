using SignUp___SignIn_Form.DTOs.Request;
using SignUp___SignIn_Form.DTOs.Response;
using SignUp___SignIn_Form.Interfaces;
using SignUp___SignIn_Form.Models;

namespace SignUp___SignIn_Form.Services
{
    public class AuthServices(IUserRepository userRepository, IConfiguration configuration, JwtServices jwtService)
        :IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtServices _jwtService = jwtService;

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) throw new Exception("Invalid Email or Password");

            var token = _jwtService.GenerateSecurityToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(double.Parse(
                    _configuration.GetSection("JwtConfig").GetSection("ExpiresInMinutes").Value!))
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var userExist = await _userRepository.UserExistance(request.Email);
            if(userExist) throw new Exception("User with this email already exists");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateUserAsync(newUser);
            var token = _jwtService.GenerateSecurityToken(createdUser);

            return new AuthResponse
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(double.Parse(
                    _configuration.GetSection("JwtConfig").GetSection("ExpiresInMinutes").Value!))
            };
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
            };
        }
    }
}
