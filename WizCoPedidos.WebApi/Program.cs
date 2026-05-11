using Microsoft.EntityFrameworkCore;
using WizCoPedidos.WebApi.Data;
using WizCoPedidos.WebApi.Repositories;
using WizCoPedidos.WebApi.Repositories.Interfaces;
using WizCoPedidos.WebApi.Services;
using WizCoPedidos.WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlite(connectionString);
});

// Repositories
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Services
builder.Services.AddScoped<IPedidoService, PedidoService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();