using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models.Staff
{
    public class Department:BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [Required]
        [MaxLength(50)]
        public string HOD { get; set; }
    }
}
