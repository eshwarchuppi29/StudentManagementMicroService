using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Response.Staff;

namespace StaffMicroService.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffResponse>> GetAllStaffAsync();
        Task<StaffResponse?> GetStaffByIdAsync(Guid id);
        Task<StaffResponse> CreateStaffAsync(StaffRequest request);
        Task<StaffResponse> UpdateStaffAsync(Guid id, StaffRequest request);
        Task<bool> DeleteStaffAsync(Guid id);
    }
}
