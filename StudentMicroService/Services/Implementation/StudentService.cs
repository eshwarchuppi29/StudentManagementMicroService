using AutoMapper;
using StudentMangementSystem.Model.Models.Student;
using StudentMangementSystem.Model.Requst.Student;
using StudentMangementSystem.Model.Response.Student;
using StudentMicroService.Repositories.Interfaces;
using StudentMicroService.Services.Interface;

namespace StudentMicroService.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentResponse>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentResponse>>(students);
        }

        public async Task<StudentResponse?> GetByIdAsync(Guid id)
        {
            var studentResponse = await _repository.GetByIdAsync(id);
            return _mapper.Map<StudentResponse>(studentResponse);
        }

        public async Task<StudentResponse> CreateAsync(StudentRequest student)
        {
            // Mapper
            var std = _mapper.Map<Student>(student);
            var studentResponse = await _repository.CreateAsync(std);
            return _mapper.Map<StudentResponse>(studentResponse);
        }

        public async Task<StudentResponse> UpdateAsync(StudentRequest student)
        {
            var studentResponse = await _repository.UpdateAsync(_mapper.Map<Student>(student));
            return _mapper.Map<StudentResponse>(studentResponse);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
