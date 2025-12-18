using StudentMangementSystem.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Models.Student
{
    public class Student
    {
        public Guid Id { get; set; }= new Guid();
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AdharNumber { get; set; }
        public string Address { get; set; }
        public bool IsArchived { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}