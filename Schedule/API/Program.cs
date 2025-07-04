using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // ������� �����������
    .WriteTo.Console()          // � �������
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // � ���� (���� ���� � ����)
    .CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });

    // ��������� ��������� ��������� sid
    c.AddSecurityDefinition("sid", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "sid",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Session ID"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "sid"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ����������� � PostgreSQL ����� ������ �����������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������ ����������� �����������
builder.Services.AddScoped<IRepository<Worker>, WorkerRepository>();
builder.Services.AddScoped<IRepository<Reception>, ReceptionRepository>();
builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IRepository<Holiday>, HolidayRepository>();
builder.Services.AddScoped<IRepository<JobTitle>, JobTitleRepository>();
builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

app.Run();
