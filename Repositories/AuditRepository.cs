using CoworkingReservationSystem.Data;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AppDbContext _context;

        public AuditRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Audit> GetByIdAsync(int id)
        {
            return await _context.Audits
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Audit>> GetAllAsync()
        {
            return await _context.Audits
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Audit>> GetByUserIdAsync(int userId)
        {
            return await _context.Audits
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Audit>> GetByEntityNameAsync(string entityName)
        {
            return await _context.Audits
                .Include(a => a.User)
                .Where(a => a.EntityName == entityName)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<Audit> CreateAsync(Audit audit)
        {
            await _context.Audits.AddAsync(audit);
            await _context.SaveChangesAsync();
            return audit;
        }
    }
}
