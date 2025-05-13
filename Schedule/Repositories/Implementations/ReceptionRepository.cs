using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories;

public class ReceptionRepository : BaseRepository<Reception>
{
    public ReceptionRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Reception>> GetAllAsync()
    {
        return await _dbSet
            .Include(r => r.requiredPersonnel)
            .Include(r => r.department)
            .Include(r => r.personnel)
                .ThenInclude(p => p.jobTitle)
            .ToListAsync();
    }

    public override async Task<Reception?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(r => r.requiredPersonnel)
            .Include(r => r.department)
            .Include(r => r.personnel)
                .ThenInclude(p => p.jobTitle)
            .FirstOrDefaultAsync(r => r.id == id);
    }
}
