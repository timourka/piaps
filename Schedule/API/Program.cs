using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Implementations;
using Repositories.Interfaces;
using Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });

    // Добавляем поддержку заголовка sid
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

// Подключение к PostgreSQL через строку подключения
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Пример регистрации репозитория
builder.Services.AddScoped<IRepository<Worker>, GenericRepository<Worker>>();
builder.Services.AddScoped<IRepository<Reception>, GenericRepository<Reception>>();
builder.Services.AddScoped<IRepository<Department>, GenericRepository<Department>>();
builder.Services.AddScoped<IRepository<Holiday>, GenericRepository<Holiday>>();
builder.Services.AddScoped<IRepository<JobTitle>, GenericRepository<JobTitle>>();

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

app.Run();
