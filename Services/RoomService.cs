using CoworkingReservationSystem.Services.Interfaces;
using CoworkingReservationSystem.Models.DTOs.Requests;
using CoworkingReservationSystem.Models.DTOs.Responses;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;

namespace CoworkingReservationSystem.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IAuditService _auditService;

        public RoomService(IRoomRepository roomRepository, IAuditService auditService)
        {
            _roomRepository = roomRepository;
            _auditService = auditService;
        }

        public async Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request)
        {
            var room = new Room
            {
                Name = request.Name,
                Description = request.Description,
                Capacity = request.Capacity,
                HasProjector = request.HasProjector,
                HasWhiteboard = request.HasWhiteboard,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdRoom = await _roomRepository.CreateAsync(room);

            await _auditService.CreateAuditAsync(
                null,
                "Room",
                createdRoom.Id.ToString(),
                AuditAction.Create,
                null,
                $"Room created: {createdRoom.Name}"
            );

            return MapToRoomResponse(createdRoom);
        }

        public async Task<IEnumerable<RoomResponse>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
            {
                throw new InvalidOperationException("Start time must be before end time");
            }

            var availableRooms = await _roomRepository.GetAvailableRoomsAsync(startTime, endTime);
            return availableRooms.Select(MapToRoomResponse);
        }

        public async Task<RoomResponse> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found");
            }

            return MapToRoomResponse(room);
        }

        public async Task<IEnumerable<RoomResponse>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(MapToRoomResponse);
        }

        private RoomResponse MapToRoomResponse(Room room)
        {
            return new RoomResponse
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Capacity = room.Capacity,
                HasProjector = room.HasProjector,
                HasWhiteboard = room.HasWhiteboard,
                CreatedAt = room.CreatedAt
            };
        }
    }
}