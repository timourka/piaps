using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
