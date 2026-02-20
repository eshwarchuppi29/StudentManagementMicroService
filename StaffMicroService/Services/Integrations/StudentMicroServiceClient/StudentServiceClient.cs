using Microsoft.Extensions.Options;
using StudentMangementSystem.Model.Response;
using StudentMangementSystem.Model.Response.Student;
using System.Net.Http.Headers;

namespace StaffMicroService.Services.Integrations.StudentMicroService
{
    public class StudentServiceClient : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly StudentServiceSettings _settings;
        private readonly IHttpContextAccessor _contextAccessor;

        public StudentServiceClient(HttpClient httpClient, IOptions<StudentServiceSettings> settings, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _contextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        public async Task<ApiResponse<List<StudentResponse>>> GetAllStudents()
        {
            AttachBearerToken();

            var endpoint = _settings.Endpoints.GetAllStudents;
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var data= await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentResponse>>>();
            return data;
        }

        private void AttachBearerToken()
        {
            var authHeader = _contextAccessor.HttpContext?.Request
                                .Headers["Authorization"]
                                .FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
