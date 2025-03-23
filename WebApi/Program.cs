using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces; 
using Services; 

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IOrdenDigitalService, OrdenDigitalService>();
builder.Services.AddScoped<IOrdenFisicaService, OrdenFisicaService>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Usar redirección HTTPS y autorización
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear controladores para que las rutas se resuelvan
app.MapControllers();


app.Run();
