
using CQRS_mediatR.Repository;
using CQRS_mediatR.Infrastructure.EmailSender;
using CQRS_mediatR.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CQRS_mediatR.Infrastructure.Middleware;
using CQRS_mediatR.Domain.Interfaces;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Add Entity Framework services first
builder.Services.AddDbContext<GamePlayerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Host.UseSerilog();
builder.Services.AddControllers();

// Add application services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IGamePlayerRepository, GamePlayerRepository>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GamePlayerCQRS API");
    });
}


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

