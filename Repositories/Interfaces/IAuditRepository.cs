using CoworkingReservationSystem.Models;

namespace CoworkingReservationSystem.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task<Audit> GetByIdAsync(int id);
        Task<IEnumerable<Audit>> GetAllAsync();
        Task<IEnumerable<Audit>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Audit>> GetByEntityNameAsync(string entityName);
        Task<Audit> CreateAsync(Audit audit);
    }
}
