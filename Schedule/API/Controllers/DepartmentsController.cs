using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

[ApiController]
[Route("api/department")]
public class DepartmentsController : ControllerBase
{
    private readonly IRepository<Department> _repo;
    private readonly IRepository<Worker> _workerRepo;
    private readonly IRepository<Holiday> _holidayRepo;
    private readonly IRepository<Reception> _receptionRepo;

    public DepartmentsController(IRepository<Department> repo, IRepository<Worker> workerRepo, IRepository<Holiday> holidayRepo, IRepository<Reception> receptionRepo)
    {
        _repo = repo;
        _workerRepo = workerRepo;
        _holidayRepo = holidayRepo;
        _receptionRepo = receptionRepo;
    }

    [HttpGet("get-all")]
    [AuthorizeWithSid]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("get/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost("add")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Add([FromBody] Department item)
    {
        if (item.workers != null)
        {
            for (int i = 0; i < item.workers.Count; i++)
            {
                var existingWorker = await _workerRepo.GetByIdAsync(item.workers[i].id);

                if (existingWorker != null)
                {
                    item.workers[i] = existingWorker;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }
        await _repo.AddAsync(item);
        await _repo.SaveAsync();
        return Ok(item);
    }

    [HttpPut("update/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Update(int id, [FromBody] Department updated)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.name = updated.name;
        existing.workSchedules = updated.workSchedules;
        existing.workers = updated.workers;

        if (existing.workers != null)
        {
            for (int i = 0; i < existing.workers.Count; i++)
            {
                var existingWorker = await _workerRepo.GetByIdAsync(existing.workers[i].id);

                if (existingWorker != null)
                {
                    existing.workers[i] = existingWorker;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }

        await _repo.UpdateAsync(existing);
        await _repo.SaveAsync();
        return Ok(existing);
    }

    [HttpDelete("delete/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _repo.DeleteAsync(existing);
        await _repo.SaveAsync();
        return Ok(new { message = "Department deleted" });
    }

    /// <summary>
    /// жесть
    /// </summary>
    /// <returns></returns>
    [HttpPost("generate-schedule/{departmentId}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> GenerateSchedule(int departmentId)
    {
        var department = await _repo.GetByIdAsync(departmentId);
        if (department == null)
            return NotFound(new { error = "Department not found" });

        var receptions = (await _receptionRepo.GetAllAsync())
            .Where(r => r.department?.id == departmentId && !r.date.HasValue)
            .ToList();

        var holidays = await _holidayRepo.GetAllAsync();
        var allWorkers = department.workers;

        List<Reception> scheduled = new();
        foreach (var reception in receptions)
        {
            for (int offset = 0; offset < 7; offset++) // пробуем 7 дней вперёд
            {
                var targetDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(offset));

                // Праздник?
                if (holidays.Any(h => h.date == targetDate))
                    continue;

                var dayOfWeek = (int)targetDate.DayOfWeek;
                if (dayOfWeek == 0) dayOfWeek = 7; // Sunday to 7

                var schedule = department.workSchedules.ElementAtOrDefault(dayOfWeek - 1);
                if (schedule == null || !schedule.isWorking)
                    continue;

                List<Worker> workingWorkers = allWorkers.Where(
                    w => w.vacations == null || !w.vacations.Any(
                        v => v != null && v.days.Any(
                            d => d.Equals(targetDate)
                            )
                        )
                    ).ToList();

                // предположим что у нас отрезки времени это пол часа
                for (TimeOnly time = schedule.startOfWork; time < schedule.endOfWork; time = time.Add(new TimeSpan(0, 30, 0)))
                {
                    List<Worker> freeWorkers = workingWorkers.Where(
                        w => !receptions.Any(
                            r =>
                            r.personnel.Contains(w) &&
                            r.date == targetDate &&
                            r.startTime < time &&
                            r.endTime > time &&
                            r.startTime < time.Add(reception.time) &&
                            r.endTime > time.Add(reception.time)
                            )
                        ).ToList();
                    foreach(JobTitle job in reception.requiredPersonnel)
                    {
                        Worker ourWorker = freeWorkers.FirstOrDefault(w => w.jobTitle == job);
                        if (ourWorker == null)
                            break;
                        reception.personnel.Add(ourWorker);
                    }
                    if (reception.personnel.Count == reception.requiredPersonnel.Count)
                    {
                        reception.date = targetDate;
                        reception.startTime = time;
                        await _receptionRepo.UpdateAsync(reception);
                        scheduled.Add(reception);
                        break;
                    }
                    reception.personnel.Clear();
                }

                if (reception.personnel.Count == reception.requiredPersonnel.Count)
                {
                    break;
                }
            }
        }

        await _receptionRepo.SaveAsync();
        return Ok(scheduled);
    }

    [HttpGet("get-schedule-for-period")]
    [AuthorizeWithSid]
    public async Task<IActionResult> GetScheduleForPeriod(
    [FromQuery] int departmentId,
    [FromQuery] DateOnly start,
    [FromQuery] DateOnly end)
    {
        var department = await _repo.GetByIdAsync(departmentId);
        if (department == null)
            return NotFound(new { error = "Department not found" });

        var receptions = await _receptionRepo.GetAllAsync();

        var filtered = receptions
            .Where(r =>
                r.department != null &&
                r.department.id == departmentId &&
                r.date.HasValue &&
                r.date.Value >= start &&
                r.date.Value <= end)
            .ToList();

        return Ok(filtered);
    }
}
