using StudentMangementSystem.Model.Response;
using StudentMangementSystem.Model.Response.Student;

namespace StaffMicroService.Services.Integrations.StudentMicroService
{
    public interface IStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetAllStudents();
    }
}
