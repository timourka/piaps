using Microsoft.AspNetCore.Mvc;
using Models;
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
