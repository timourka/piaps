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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка каскадного удаления JobTitle → Worker
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.jobTitle)
                .WithMany() // или .WithMany(j => j.Workers), если в JobTitle есть список
                .HasForeignKey("JobTitleId") // явный внешний ключ
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
