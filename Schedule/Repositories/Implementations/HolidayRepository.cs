using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories;

public class HolidayRepository : BaseRepository<Holiday>
{
    public HolidayRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Holiday>> GetAllAsync() => await _dbSet.ToListAsync();
    public override async Task<Holiday?> GetByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(h => h.id == id);
}
