using StaffMicroService.DatabaseContext;
using StaffMicroService.Repositories.Interfaces;
using StudentMangementSystem.Model.Models;

namespace StaffMicroService.Repositories.Implementation
{
    public class ApiLogRepository : IApiLogRepository
    {
        private readonly StaffDbContext _context;

        public ApiLogRepository(StaffDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ApiLog log)
        {
            _context.ApiLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
