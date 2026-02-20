using StudentMangementSystem.Model.Response.Student;

namespace StaffMicroService.Services.Integrations.StudentMicroService
{
    public interface IStudentService
    {
        Task<StudentResponse> GetAllStudents();
    }
}
