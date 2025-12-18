using StudentMangementSystem.Model.Models.Student;
using StudentMangementSystem.Model.Requst.Student;
using StudentMangementSystem.Model.Response.Student;

namespace StudentMicroService.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponse>> GetAllAsync();
        Task<StudentResponse?> GetByIdAsync(Guid id);
        Task<StudentResponse> CreateAsync(StudentRequest student);
        Task<StudentResponse> UpdateAsync(StudentRequest student);
        Task<bool> DeleteAsync(Guid id);
    }
}
