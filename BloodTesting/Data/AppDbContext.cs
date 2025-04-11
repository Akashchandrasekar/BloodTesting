using Microsoft.EntityFrameworkCore;
using BloodTesting.Models;

namespace BloodTesting.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MedicalTest> MedicalTests { get; set; }
    }
}
