using StudentMangementSystem.Model.BaseRequestResponse;
using StudentMangementSystem.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Requst.Staff
{
    public class StaffRequest:BaseStaffRequestResponse
    {
        public Guid DepartmentId { get; set; }
    }
}
