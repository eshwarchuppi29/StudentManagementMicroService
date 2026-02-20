using StudentMangementSystem.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models.Staff
{
    public class Staff : BaseModel
    {
        [Required]
        [MaxLength(5)]
        public int Salutation { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [MaxLength(6)]
        public int Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string MobileNumber { get; set; }

        [MaxLength(10)]
        public string? AlterMobileNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string MotherName { get; set; } = string.Empty;

        [Required]
        [MaxLength(12)]
        public string AdharNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string PANNumber { get; set; } = string.Empty;

        [Required]
        public bool IsPermanent { get; set; } = true;

        [Required]
        public bool IsTeachingFaculty { get; set; }=true;

        [Required]
        [MaxLength(50)]
        public string? Qualification { get;set; }

        [Required]
        [MaxLength(50)]
        public string? Designation { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public Department Departments { get; set; } = new Department(); //Navigation property
    }
}