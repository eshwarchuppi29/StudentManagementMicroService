using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models
{
    public class ApiLog
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string? TraceId { get; set; }
        public string? UserId { get; set; }

        public string? RequestPath { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestBody { get; set; }

        public int StatusCode { get; set; }
        public string? ResponseBody { get; set; }

        public string? ExceptionMessage { get; set; }
        public string? ExceptionStackTrace { get; set; }

        public long ExecutionTimeMs { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}