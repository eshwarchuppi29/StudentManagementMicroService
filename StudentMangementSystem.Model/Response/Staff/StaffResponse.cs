using StudentMangementSystem.Model.BaseRequestResponse;
using StudentMangementSystem.Model.Enums;
using StudentMangementSystem.Model.Models.Staff;
using StudentMangementSystem.Model.Requst.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Response.Staff
{
    public class StaffResponse : BaseStaffRequestResponse
    {
        public DepartmentResponse Departments { get; set; }
    }
}
