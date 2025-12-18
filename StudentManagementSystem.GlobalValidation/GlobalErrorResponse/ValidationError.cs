using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.GlobalValidation.GlobalErrorResponse
{
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
