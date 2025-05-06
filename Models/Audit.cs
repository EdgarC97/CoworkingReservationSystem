using System;

namespace CoworkingReservationSystem.Models
{
    public enum AuditAction
    {
        Create,
        Update,
        Delete,
        Login,
        Register,
        Cancel
    }

    public class Audit
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}