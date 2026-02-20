using Microsoft.Extensions.Options;
using StudentMangementSystem.Model.Response.Student;

namespace StaffMicroService.Services.Integrations.StudentMicroService
{
    public class StudentServiceClient : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly StudentServiceSettings _settings;

        public StudentServiceClient(HttpClient httpClient, IOptions<StudentServiceSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        public async Task<StudentResponse> GetAllStudents()
        {
            var endpoint = _settings.Endpoints.GetAllStudents;
            var response = await _httpClient.GetAsync(endpoint);
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<StudentResponse>();
            }
            else
            {

            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StudentResponse>();
        }
    }
}
