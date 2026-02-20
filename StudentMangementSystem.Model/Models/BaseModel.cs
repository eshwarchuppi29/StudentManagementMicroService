using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
