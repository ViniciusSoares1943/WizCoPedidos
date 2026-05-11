using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection;
using System.Text.Json.Serialization;
using WizCoPedidos.WebApi.Data;
using WizCoPedidos.WebApi.Mappings;
using WizCoPedidos.WebApi.Middlewares;
using WizCoPedidos.WebApi.Repositories;
using WizCoPedidos.WebApi.Repositories.Interfaces;
using WizCoPedidos.WebApi.Services;
using WizCoPedidos.WebApi.Services.Interfaces;
using WizCoPedidos.WebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, _, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console(new RenderedCompactJsonFormatter());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddAutoMapper(typeof(PedidoMappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PedidoCriacaoRequestValidator>();
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
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseGlobalExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
