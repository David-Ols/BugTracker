using BugAPI.Repository;
using BugAPI.Repository.Interfaces;
using BugAPI.Services;
using BugAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddScoped<IBugRepository, BugRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

