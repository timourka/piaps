using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories;

public class WorkerRepository : BaseRepository<Worker>
{
    public WorkerRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Worker>> GetAllAsync()
    {
        return await _dbSet
            .Include(w => w.jobTitle)
            .Include(w => w.vacations)
                .ThenInclude(v => v.days)
            .Include(w => w.departments)
            .ToListAsync();
    }

    public override async Task<Worker?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(w => w.jobTitle)
            .Include(w => w.vacations)
                .ThenInclude(v => v.days)
            .Include(w => w.departments)
            .FirstOrDefaultAsync(w => w.id == id);
    }
}
