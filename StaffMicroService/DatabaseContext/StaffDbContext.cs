using Microsoft.EntityFrameworkCore;
using StudentMangementSystem.Model.Models;
using StudentMangementSystem.Model.Models.Staff;
using System.Xml;

namespace StaffMicroService.DatabaseContext
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<ApiLog> ApiLogs { get; set; }
    }
}
