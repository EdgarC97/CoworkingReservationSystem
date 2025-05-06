using CoworkingReservationSystem.Models.DTOs.Requests;
using CoworkingReservationSystem.Models.DTOs.Responses;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;
using CoworkingReservationSystem.Services.Interfaces;
using CoworkingReservationSystem.Utils;

namespace CoworkingReservationSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository, IAuditService auditService, JwtService jwtService)
        {
            _userRepository = userRepository;
            _auditService = auditService;
            _jwtService = jwtService;
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            await _auditService.CreateAuditAsync(
                createdUser.Id,
                "User",
                createdUser.Id.ToString(),
                AuditAction.Register,
                null,
                $"User registered: {createdUser.Email}"
            );

            return new UserResponse
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                CreatedAt = createdUser.CreatedAt
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);

            await _auditService.CreateAuditAsync(
                user.Id,
                "User",
                user.Id.ToString(),
                AuditAction.Login,
                "",
                $"User logged in: {user.Email}"
            );

            return new LoginResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedAt = user.CreatedAt
                }
            };
        }
    }
}