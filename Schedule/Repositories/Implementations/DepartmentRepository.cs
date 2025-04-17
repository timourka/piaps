using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories;

public class DepartmentRepository : BaseRepository<Department>
{
    public DepartmentRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _dbSet
            .Include(d => d.workSchedules)
            .Include(d => d.workers)
                .ThenInclude(w => w.jobTitle)
            .Include(d => d.workers)
                .ThenInclude(w => w.vacations)
                    .ThenInclude(v => v.days)
            .ToListAsync();
    }

    public override async Task<Department?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(d => d.workSchedules)
            .Include(d => d.workers)
                .ThenInclude(w => w.jobTitle)
            .Include(d => d.workers)
                .ThenInclude(w => w.vacations)
                    .ThenInclude(v => v.days)
            .FirstOrDefaultAsync(d => d.id == id);
    }
}
