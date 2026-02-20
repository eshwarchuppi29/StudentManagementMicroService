using Microsoft.EntityFrameworkCore;
using StudentMangementSystem.Model.Models;
using StudentMangementSystem.Model.Models.Staff;
using System.Xml;

namespace StaffMicroService.DatabaseContext
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>().HasOne(s => s.Departments)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Staff>().HasIndex(s => s.MobileNumber);
            modelBuilder.Entity<Staff>().HasIndex(s => s.AdharNumber);
            modelBuilder.Entity<Staff>().HasIndex(s => s.PANNumber);

            modelBuilder.Entity<Department>().HasIndex(d => d.DepartmentName);

        }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<ApiLog> ApiLogs { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
