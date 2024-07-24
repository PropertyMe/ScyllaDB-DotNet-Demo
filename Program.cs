using ScyllaDBDemo.Models;
using ScyllaDBDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactServices, ContactServices>();
builder.Services.AddTransient<IMigrationService, MigrationService>();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

var app = builder.Build();

if (settings.RunMigrations)
{
    var migrationService = app.Services.GetRequiredService<IMigrationService>();
    migrationService.Execute();
}

if (settings.SeedData)
{
    var migrationService = app.Services.GetRequiredService<IMigrationService>();
    migrationService.Seed();
}

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
