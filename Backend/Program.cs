using BugTracker.Repository;
using BugTracker.Repository.Interfaces;
using BugTracker.Services;
using BugTracker.Services.Interfaces;
using BugTracker.Mappers;
using BugTracker.Mappers.Interfaces;
using BugTracker.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:8080", "http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.Configure<APISettings>(builder.Configuration.GetSection("APISettings"));
builder.Services.AddControllers();

builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBugMapper, BugMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

