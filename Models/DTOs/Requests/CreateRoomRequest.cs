namespace CoworkingReservationSystem.Models.DTOs.Requests
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }
    }
}