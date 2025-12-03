using EstacionamientoACE_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // <- necesario para ReferenceHandler

var builder = WebApplication.CreateBuilder(args);

// 🧩 Conexión a la Base de Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // 🔹 Ignorar ciclos de referencia al serializar
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // Opcional: JSON más legible
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🌐 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.WebHost.UseUrls("https://localhost:7333", "http://0.0.0.0:5333");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
