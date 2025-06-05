using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Repositories.Interfaces;

[ApiController]
[Route("api/reception")]
public class ReceptionsController : ControllerBase
{
    private readonly IRepository<Reception> _repo;
    private readonly IRepository<Worker> _repoWorker;
    private readonly IRepository<JobTitle> _repoJobTitle;
    private readonly IRepository<Department> _repoDepartment;

    public ReceptionsController(IRepository<Reception> repo, IRepository<Worker> repoWorker, IRepository<JobTitle> repoJobTitle, IRepository<Department> repoDepartment)
    {
        _repo = repo;
        _repoWorker = repoWorker;
        _repoJobTitle = repoJobTitle;
        _repoDepartment = repoDepartment;

        
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

    [HttpGet("getByLogin/{login}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> GetByLogin(string login)
    {
        var sid = HttpContext.Request.Headers["sid"].FirstOrDefault();

        var workers = await _repoWorker.GetAllAsync();
        if (workers is null)
            return NotFound();

        var worker = workers.FirstOrDefault(w => w.login == login);
        if (worker is null)
            return NotFound();

        if (worker.id != SessionStore.Sessions[sid])
            return NotFound();

        var receptions = await _repo.GetAllAsync();
        var filteredReceptions = receptions.Where(r => r.personnel != null && r.personnel.Contains(worker)).ToList();

        return filteredReceptions is null ? NotFound() : Ok(filteredReceptions);
    }

    [HttpPost("add")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Add([FromBody] Reception item)
    {
        if (item.personnel != null)
        {
            for (int i = 0; i < item.personnel.Count; i++)
            {
                var existingWorker = await _repoWorker.GetByIdAsync(item.personnel[i].id);

                if (existingWorker != null)
                {
                    item.personnel[i] = existingWorker;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }
        if (item.requiredPersonnel != null)
        {
            for (int i = 0; i < item.requiredPersonnel.Count; i++)
            {
                var existingJob = await _repoJobTitle.GetByIdAsync(item.requiredPersonnel[i].id);

                if (existingJob != null)
                {
                    item.requiredPersonnel[i] = existingJob;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }
        if (item.department != null)
        {
            var existingDepartment = await _repoDepartment.GetByIdAsync(item.department.id);

            if (existingDepartment != null)
            {
                item.department = existingDepartment;
            }
            else
            {
                // Можно либо вернуть ошибку, либо создать новую должность
                return BadRequest(new { error = "Invalid jobTitle id" });
            }
        }
        await _repo.AddAsync(item);
        await _repo.SaveAsync();
        return Ok(item);
    }

    [HttpPost("add-dto")]
    [AuthorizeWithSid]
    public async Task<IActionResult> AddDto([FromBody] ReceptionCreateDto dto)
    {
        // Получаем отделение
        var department = await _repoDepartment.GetByIdAsync(dto.departmentId);
        if (department == null)
            return BadRequest(new { error = "Invalid department id" });

        // Получаем сотрудников
        var personnel = new List<Worker>();
        foreach (var workerId in dto.personnelIds)
        {
            var worker = await _repoWorker.GetByIdAsync(workerId);
            if (worker == null)
                return BadRequest(new { error = $"Invalid worker id: {workerId}" });
            personnel.Add(worker);
        }

        // Получаем требуемые должности
        var requiredPersonnel = new List<JobTitle>();
        foreach (var jobTitleId in dto.requiredPersonnelIds)
        {
            var jobTitle = await _repoJobTitle.GetByIdAsync(jobTitleId);
            if (jobTitle == null)
                return BadRequest(new { error = $"Invalid job title id: {jobTitleId}" });
            requiredPersonnel.Add(jobTitle);
        }

        // Формируем новый приём
        var reception = new Reception
        {
            date = dto.date,
            startTime = dto.startTime,
            time = dto.time,
            department = department,
            personnel = personnel,
            requiredPersonnel = requiredPersonnel
        };

        await _repo.AddAsync(reception);
        await _repo.SaveAsync();

        return Ok(reception);
    }


    [HttpPut("update/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Update(int id, [FromBody] Reception updated)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.time = updated.time;
        existing.requiredPersonnel = updated.requiredPersonnel;
        existing.personnel = updated.personnel;
        existing.department = updated.department;
        existing.date = updated.date;
        existing.startTime = updated.startTime;
        if (existing.personnel != null)
        {
            for (int i = 0; i < existing.personnel.Count; i++)
            {
                var existingWorker = await _repoWorker.GetByIdAsync(existing.personnel[i].id);

                if (existingWorker != null)
                {
                    existing.personnel[i] = existingWorker;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }
        if (existing.requiredPersonnel != null)
        {
            for (int i = 0; i < existing.requiredPersonnel.Count; i++)
            {
                var existingJob = await _repoJobTitle.GetByIdAsync(existing.requiredPersonnel[i].id);

                if (existingJob != null)
                {
                    existing.requiredPersonnel[i] = existingJob;
                }
                else
                {
                    // Можно либо вернуть ошибку, либо создать новую должность
                    return BadRequest(new { error = "Invalid jobTitle id" });
                }
            }
        }
        if (existing.department != null)
        {
            var existingDepartment = await _repoDepartment.GetByIdAsync(existing.department.id);

            if (existingDepartment != null)
            {
                existing.department = existingDepartment;
            }
            else
            {
                // Можно либо вернуть ошибку, либо создать новую должность
                return BadRequest(new { error = "Invalid jobTitle id" });
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
        return Ok(new { message = "Reception deleted" });
    }
}
