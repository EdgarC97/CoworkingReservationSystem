using CoworkingReservationSystem.DTOs.Responses;
using CoworkingReservationSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoworkingReservationSystem.Interfaces.Services
{
    public interface IAuditService
    {
        Task<AuditResponse> CreateAuditAsync(int? userId, string entityName, string entityId, AuditAction action, string oldValues = null, string newValues = null);
        Task<IEnumerable<AuditResponse>> GetAllAuditsAsync();
        Task<IEnumerable<AuditResponse>> GetAuditsByUserIdAsync(int userId);
        Task<IEnumerable<AuditResponse>> GetAuditsByEntityNameAsync(string entityName);
    }
}