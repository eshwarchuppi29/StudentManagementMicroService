using AutoMapper;
using StudentMangementSystem.Model.Enums;
using StudentMangementSystem.Model.Models.Staff;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Response.Staff;

namespace StaffMicroService.Mapper
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<StaffRequest, Staff>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Staff, StaffResponse>().ReverseMap();

            CreateMap<Department, DepartmentResponse>().ReverseMap();
        }
    }
}
