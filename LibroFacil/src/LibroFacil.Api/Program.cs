using LibroFacil.Application.Interfaces;
using LibroFacil.Application.Services;
using LibroFacil.Infrastructure.Persistence;
using LibroFacil.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext -> SQL Server
builder.Services.AddDbContext<LibroFacilDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibroFacilDb")));

// Inyección de dependencias: Application depende de abstracciones (DIP)
builder.Services.AddScoped<ILibroRepository, LibroRepositoryEf>();
builder.Services.AddScoped<LibroService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();