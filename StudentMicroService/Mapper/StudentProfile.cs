using AutoMapper;
using StudentMangementSystem.Model.Models.Student;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Requst.Student;
using StudentMangementSystem.Model.Response.Student;

namespace StudentMicroService.Mapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentRequest, Student>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsArchived, opt => opt.MapFrom(src => false));
            CreateMap<Student, StudentResponse>();
        }
    }
}
