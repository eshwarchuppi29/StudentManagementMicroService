using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string CreatedBy { get; set; } = "System";

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
