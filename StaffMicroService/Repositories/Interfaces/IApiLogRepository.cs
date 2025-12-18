using StudentMangementSystem.Model.Models;

namespace StaffMicroService.Repositories.Interfaces
{
    public interface IApiLogRepository
    {
        Task SaveAsync(ApiLog log);
    }
}
