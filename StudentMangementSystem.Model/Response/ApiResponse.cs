using StudentMangementSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public T ValidaitonErrors { get; set; }
        public List<ErrorDetail> Errors { get; set; } = new();

        public object Exception { get; set; }
    }
}