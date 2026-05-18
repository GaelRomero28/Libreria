using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Libreria.Application.Interfaces;
using Libreria.Application.Services;
using Libreria.Domain.Interfaces;
using Libreria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Servicios base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Configurar Swagger con comentarios XML
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Libreria.API", Version = "v1" });

    // Archivo XML principal de la API
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);

    // Archivo XML de la capa Application (para DTOs)
    var xmlFileApp = "Libreria.Application.xml";
    var xmlPathApp = Path.Combine(AppContext.BaseDirectory, xmlFileApp);
    if (File.Exists(xmlPathApp)) c.IncludeXmlComments(xmlPathApp);
});

// Configurar comportamiento de Data Annotations para retornar 422 Unprocessable Entity
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new UnprocessableEntityObjectResult(context.ModelState);
    };
});

// 3. Configurar CORS (Política permisiva para desarrollo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Inyección de Dependencias: Configuración de SQL Server para Dapper
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                          ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

// Inyección de Repositorios y Servicios
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

var app = builder.Build();

// 5. Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
