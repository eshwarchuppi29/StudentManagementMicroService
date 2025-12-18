using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.GlobalValidation.GlobalErrorResponse
{
    public class ValidationErrorResponse
    {
        public List<ValidationError> Errors { get; set; } = new();
    }
}
