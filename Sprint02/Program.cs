using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.Service;
using Sprint02.DTOs;
using Oracle.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IService<MotoResponseDto, MotoRequestDto>, MotoService>();
builder.Services.AddScoped<IService<StatusMotoResponseDto, StatusMotoRequestDto>, StatusMotoService>();
builder.Services.AddScoped<IService<TipoMotoResponseDto, TipoMotoRequestDto>, TipoMotoService>();
builder.Services.AddScoped<IService<UsuarioResponseDto, UsuarioRequestDto>, UsuarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
