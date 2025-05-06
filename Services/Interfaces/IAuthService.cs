using CoworkingReservationSystem.Models.DTOs.Responses;

using Microsoft.AspNetCore.Identity.Data;

namespace CoworkingReservationSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
