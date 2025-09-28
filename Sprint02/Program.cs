using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.Service;
using Sprint02.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IService<MotoResponseDto, MotoRequestDto>, MotoService>();
builder.Services.AddScoped<IService<StatusMotoResponseDto, StatusMotoRequestDto>, StatusMotoService>();
builder.Services.AddScoped<IService<TipoMotoResponseDto, TipoMotoRequestDto>, TipoMotoService>();
builder.Services.AddScoped<IService<UsuarioResponseDto, UsuarioRequestDto>, UsuarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API SmartMottu",
        Version = "v1",
        Description = "API para gerenciar Usuários, Motos, Tipos e Status para a empresa Mottu"
    });

    c.EnableAnnotations();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API SmartMottu v1");
    c.RoutePrefix = string.Empty; 
});



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
