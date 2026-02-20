using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.BaseRequestResponse
{
    public class BaseStaffRequestResponse
    {
        public int Salutation { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; }
        public string? AlterMobileNumber { get; set; }
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string AdharNumber { get; set; } = string.Empty;
        public string PANNumber { get; set; } = string.Empty;
        public bool IsPermanent { get; set; } = true;
        public bool IsTeachingFaculty { get; set; }
        public string? Qualification { get; set; }
        public string? Designation { get; set; }
    }
}
