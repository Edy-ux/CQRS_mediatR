using CQRS_mediatR.Application.Interfaces;
using CQRS_mediatR.Repository;
using CQRS_mediatR.Infrastructure.EmailSender;
using CQRS_mediatR.Data;
using Microsoft.EntityFrameworkCore;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Add Entity Framework services first
builder.Services.AddDbContext<GamePlayerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Host.UseSerilog();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add application services
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IGamePlayerRepository, GamePlayerRepository>();

//MediatR


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GamePlayerCQRS API");
    });
}

// app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
