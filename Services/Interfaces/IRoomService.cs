using CoworkingReservationSystem.Models.DTOs.Requests;
using CoworkingReservationSystem.Models.DTOs.Responses;

namespace CoworkingReservationSystem.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request);
        Task<IEnumerable<RoomResponse>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime);
        Task<RoomResponse> GetRoomByIdAsync(int id);
        Task<IEnumerable<RoomResponse>> GetAllRoomsAsync();
    }
}
