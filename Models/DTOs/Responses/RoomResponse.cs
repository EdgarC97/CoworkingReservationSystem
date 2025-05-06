namespace CoworkingReservationSystem.Models.DTOs.Responses
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}