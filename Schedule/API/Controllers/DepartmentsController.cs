﻿using Microsoft.AspNetCore.Mvc;
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
            .Where(r => r.department?.id == departmentId)
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
                    w => !w.vacations.Any(
                        v => v.days.Any(
                            d => d.Equals(targetDate)
                            )
                        )
                    ).ToList();

                foreach(var job in reception.requiredPersonnel)
                {
                    var matchingWorker = workingWorkers.FirstOrDefault(w => w.jobTitle == job);
                    if (matchingWorker != null)
                        reception.personnel.Add(matchingWorker);
                }

                if (reception.personnel.Count == reception.requiredPersonnel.Count)
                {
                    reception.date = targetDate;
                    reception.startTime = schedule.startOfWork;
                    await _receptionRepo.UpdateAsync(reception);
                    scheduled.Add(reception);
                    break;
                }
            }
        }

        await _receptionRepo.SaveAsync();
        return Ok(scheduled);
    }
}
