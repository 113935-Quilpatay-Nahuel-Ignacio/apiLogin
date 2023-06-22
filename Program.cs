using ApiLogin.Data;
using ApiLogin.Services.PersonaService.Commands;
using static ApiLogin.Services.PersonaService.Commands.LoginPersonaCommandHandler;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyOrigin()
        .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddDbContext<ApplicationContext>();


builder.Services.AddScoped<IValidator<PostPersonaCommand>, PostPersonaCommandValidation>();
builder.Services.AddScoped<IValidator<LoginPersonaCommand>, LoginPersonaCommandValidation>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(config =>
{
    config.AllowAnyHeader();
    config.AllowAnyMethod();
    config.AllowAnyOrigin();
});

app.MapControllers();

app.Run();

//NuGets:
//Microsoft.EntityFrameworkCore
//Microsoft.EntityFrameworkCore.Tools
//Newtonsoft.Json
//Npgsql.EntityFrameworkCore.PosgreSQL
//MediatR
//FluentValidation.AspNetCore
//Automapper
//AutoMapper.Extensions.Microsoft.DependencyInjection

//appsettings.json:
/*  "ConnectionStrings": {
    "connectionString": "Server=localhost;Port=5432;Database=;User Id=;Password=;"
  }*/

//Consola del administrador de paquetes:    
//Previamente crear carpetas: Models, Data
//Scaffold-DbContext "Name=connectionString" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -ContextDir Data -Context ApplicationContext
//Actualizar base de datos (estructura)
//Scaffold-DbContext "Name=connectionString" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -ContextDir Data -Context ApplicationContext -f
