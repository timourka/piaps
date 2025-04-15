using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;

[ApiController]
[Route("api/worker")]
public class WorkersController : ControllerBase
{
    private readonly IRepository<Worker> _workerRepo;
    private readonly IRepository<JobTitle> _jobTitleRepo;

    public WorkersController(IRepository<Worker> workerRepo, IRepository<JobTitle> jobTitleRepo)
    {
        _workerRepo = workerRepo;
        _jobTitleRepo = jobTitleRepo;
    }

    [HttpGet("get-all")]
    [AuthorizeWithSid]
    public async Task<IActionResult> GetAll()
    {
        var workers = await _workerRepo.GetAllAsync();
        return Ok(workers);
    }

    [HttpGet("get/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Get(int id)
    {
        var worker = await _workerRepo.GetByIdAsync(id);
        return worker is null ? NotFound() : Ok(worker);
    }

    [HttpPost("add")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Add([FromBody] Worker worker)
    {
        if (worker.jobTitle != null)
        {
            var existingJobTitle = await _jobTitleRepo.GetByIdAsync(worker.jobTitle.id);

            if (existingJobTitle != null)
            {
                worker.jobTitle = existingJobTitle;
            }
            else
            {
                // Можно либо вернуть ошибку, либо создать новую должность
                return BadRequest(new { error = "Invalid jobTitle id" });
            }
        }
        await _workerRepo.AddAsync(worker);
        await _workerRepo.SaveAsync();
        return Ok(worker);
    }

    [HttpPut("update/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Update(int id, [FromBody] Worker updated)
    {
        var existing = await _workerRepo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.name = updated.name;
        existing.login = updated.login;
        existing.password = updated.password;
        existing.jobTitle = updated.jobTitle;
        existing.vacations = updated.vacations;

        if (existing.jobTitle != null)
        {
            var existingJobTitle = await _jobTitleRepo.GetByIdAsync(existing.jobTitle.id);

            if (existingJobTitle != null)
            {
                existing.jobTitle = existingJobTitle;
            }
            else
            {
                // Можно либо вернуть ошибку, либо создать новую должность
                return BadRequest(new { error = "Invalid jobTitle id" });
            }
        }

        await _workerRepo.UpdateAsync(existing);
        await _workerRepo.SaveAsync();

        return Ok(existing);
    }

    [HttpDelete("delete/{id}")]
    [AuthorizeWithSid]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _workerRepo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _workerRepo.DeleteAsync(existing);
        await _workerRepo.SaveAsync();

        return Ok(new { message = "Worker deleted" });
    }
}
