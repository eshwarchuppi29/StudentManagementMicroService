using Microsoft.EntityFrameworkCore;
using StaffMicroService.DatabaseContext;
using StaffMicroService.Repositories.Interfaces;
using StudentMangementSystem.Model.Models.Staff;
using System.Xml;

namespace StaffMicroService.Repositories.Implementation
{
    public class StaffRepository : IStaffRepository
    {
        private readonly StaffDbContext _context;

        public StaffRepository(StaffDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staffs.Include(s=>s.Departments).ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(Guid id)
        {
            //// AsNoTracking() is used to don't track after reading the data. Otherwise we are facing DBConcurrency issue.
            //return await _context.Staffs.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            // AsNoTracking() is used to don't track after reading the data. Otherwise we are facing DBConcurrency issue.
            return await _context.Staffs.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Staff> AddAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
                return false;

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
