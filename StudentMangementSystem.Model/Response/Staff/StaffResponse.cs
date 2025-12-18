using StudentMangementSystem.Model.Enums;
using StudentMangementSystem.Model.Requst.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Response.Staff
{
    public class StaffResponse : StaffRequest
    {
        public Guid Id { get; set; }
    }
}
