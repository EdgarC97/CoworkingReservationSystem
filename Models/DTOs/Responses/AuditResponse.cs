namespace CoworkingReservationSystem.Models.DTOs.Responses
{
    public class AuditResponse
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
