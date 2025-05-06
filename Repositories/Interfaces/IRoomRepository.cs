using CoworkingReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoworkingReservationSystem.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime);
        Task<Room> CreateAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}