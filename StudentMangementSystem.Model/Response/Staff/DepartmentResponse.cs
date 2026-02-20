using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Response.Staff
{
    public class DepartmentResponse
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public string HOD {  get; set; }
    }
}
