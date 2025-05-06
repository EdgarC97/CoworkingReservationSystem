using CoworkingReservationSystem.Models.DTOs.Responses;

namespace CoworkingReservationSystem.Models.DTOs.Responses
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserResponse User { get; set; }
        public RoomResponse Room { get; set; }
    }
}
