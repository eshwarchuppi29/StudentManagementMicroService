using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StaffMicroService.Repositories.Interfaces;
using StaffMicroService.Services.Integrations.StudentMicroService;
using StaffMicroService.Services.Interfaces;
using StudentMangementSystem.Model.Models.Staff;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Response.Staff;

namespace StaffMicroService.Services.Implementation
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StaffService(IStaffRepository staffRepository, IMapper mapper, IStudentService studentService)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<IEnumerable<StaffResponse>> GetAllStaffAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();

            // Calling StudentMicroService to get the student details.
            var data = await _studentService.GetAllStudents();
            var studentData = data.Result;
            return staffList.Select(s => _mapper.Map<StaffResponse>(s));
        }

        public async Task<StaffResponse?> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            return staff == null ? null : _mapper.Map<StaffResponse>(staff);
        }

        public async Task<StaffResponse> CreateStaffAsync(StaffRequest request)
        {
            // Business logic (example)
            var staff = _mapper.Map<Staff>(request);
            staff = await _staffRepository.AddAsync(staff);

            return _mapper.Map<StaffResponse>(staff);
        }

        public async Task<StaffResponse> UpdateStaffAsync(Guid id, StaffRequest request)
        {
            var existing = await _staffRepository.GetByIdAsync(id);
            if (existing == null) 
                return null;

            var staff = _mapper.Map<Staff>(request);
            staff.Id = id;

            var updated = await _staffRepository.UpdateAsync(staff);
            return _mapper.Map<StaffResponse>(updated);
        }

        public async Task<bool> DeleteStaffAsync(Guid id)
        {
            return await _staffRepository.DeleteAsync(id);
        }
    }
}
