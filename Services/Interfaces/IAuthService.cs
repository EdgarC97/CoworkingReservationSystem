using CoworkingReservationSystem.Models.DTOs.Responses;
using CoworkingReservationSystem.Models.DTOs.Requests;


namespace CoworkingReservationSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
