using StudentMangementSystem.Model.Models.Staff;

namespace StaffMicroService.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Task<Staff> AddAsync(Staff staff);
        Task<Staff> UpdateAsync(Staff staff);
        Task<bool> DeleteAsync(Guid id);
        Task<Staff> GetByIdAsync(Guid id);
        Task<IEnumerable<Staff>> GetAllAsync();
    }
}
