using CoworkingReservationSystem.Services.Interfaces;
using CoworkingReservationSystem.Models.DTOs.Responses;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;

namespace CoworkingReservationSystem.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IUserRepository _userRepository;

        public AuditService(IAuditRepository auditRepository, IUserRepository userRepository)
        {
            _auditRepository = auditRepository;
            _userRepository = userRepository;
        }

        public async Task<AuditResponse> CreateAuditAsync(int? userId, string entityName, string entityId, AuditAction action, string oldValues = null, string newValues = null)
        {
            var audit = new Audit
            {
                UserId = userId,
                EntityName = entityName,
                EntityId = entityId,
                Action = action,
                OldValues = oldValues,
                NewValues = newValues,
                Timestamp = DateTime.UtcNow
            };

            var createdAudit = await _auditRepository.CreateAsync(audit);
            return await MapToAuditResponseAsync(createdAudit);
        }

        public async Task<IEnumerable<AuditResponse>> GetAllAuditsAsync()
        {
            var audits = await _auditRepository.GetAllAsync();
            var responses = new List<AuditResponse>();

            foreach (var audit in audits)
            {
                responses.Add(await MapToAuditResponseAsync(audit));
            }

            return responses;
        }

        public async Task<IEnumerable<AuditResponse>> GetAuditsByUserIdAsync(int userId)
        {
            var audits = await _auditRepository.GetByUserIdAsync(userId);
            var responses = new List<AuditResponse>();

            foreach (var audit in audits)
            {
                responses.Add(await MapToAuditResponseAsync(audit));
            }

            return responses;
        }

        public async Task<IEnumerable<AuditResponse>> GetAuditsByEntityNameAsync(string entityName)
        {
            var audits = await _auditRepository.GetByEntityNameAsync(entityName);
            var responses = new List<AuditResponse>();

            foreach (var audit in audits)
            {
                responses.Add(await MapToAuditResponseAsync(audit));
            }

            return responses;
        }

        private async Task<AuditResponse> MapToAuditResponseAsync(Audit audit)
        {
            string userEmail = null;

            if (audit.UserId.HasValue)
            {
                var user = audit.User ?? await _userRepository.GetByIdAsync(audit.UserId.Value);
                userEmail = user?.Email;
            }

            return new AuditResponse
            {
                Id = audit.Id,
                UserId = audit.UserId,
                UserEmail = userEmail,
                EntityName = audit.EntityName,
                EntityId = audit.EntityId,
                Action = audit.Action,
                OldValues = audit.OldValues,
                NewValues = audit.NewValues,
                Timestamp = audit.Timestamp
            };
        }
    }
}