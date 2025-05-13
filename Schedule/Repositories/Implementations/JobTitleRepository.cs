using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories;

public class JobTitleRepository : BaseRepository<JobTitle>
{
    public JobTitleRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<JobTitle>> GetAllAsync()
    {
        return await _dbSet.Include(j => j.receptions).ToListAsync();
    }

    public override async Task<JobTitle?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(j => j.receptions).FirstOrDefaultAsync(j => j.id == id);
    }
}
