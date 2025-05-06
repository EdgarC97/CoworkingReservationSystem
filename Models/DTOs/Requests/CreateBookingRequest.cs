namespace CoworkingReservationSystem.Models.DTOs.Requests
{
    public class CreateBookingRequest
    {
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}