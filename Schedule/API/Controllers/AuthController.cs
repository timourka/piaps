using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;
using System.Security.Cryptography;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRepository<Worker> _workerRepo;

    public AuthController(IRepository<Worker> workerRepo)
    {
        _workerRepo = workerRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var workers = await _workerRepo.GetAllAsync();
        var user = workers.FirstOrDefault(w => w.login == req.Login && w.password == req.Password);

        if (user == null)
            return Unauthorized(new { error = "Invalid login or password" });

        // Генерация SID
        var sid = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
        SessionStore.Sessions[sid] = user.id;

        return Ok(new { sid });
    }

    [HttpPost("logout")]
    [AuthorizeWithSid]
    public IActionResult Logout()
    {
        var sid = Request.Headers["sid"].ToString();
        SessionStore.Sessions.Remove(sid);
        return Ok(new { message = "Logged out" });
    }
}

public class LoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}
