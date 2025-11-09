using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.Service;
using Sprint02.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sprint02.NovaPasta;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IService<MotoResponseDto, MotoRequestDto>, MotoService>();
builder.Services.AddScoped<IService<StatusMotoResponseDto, StatusMotoRequestDto>, StatusMotoService>();
builder.Services.AddScoped<IService<TipoMotoResponseDto, TipoMotoRequestDto>, TipoMotoService>();
builder.Services.AddScoped<IService<UsuarioResponseDto, UsuarioRequestDto>, UsuarioService>();

builder.Services.AddControllers();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("API OK"))
    .AddOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
name: "OracleDB-Check",
        tags: new[] { "database", "oracle" }
    );



builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSingleton<TreinamentoService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT").GetSection("Key").Value);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    c.EnableAnnotations();
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
    }
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/ping", () => Results.Ok("pong"));

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
});

app.MapControllers();

app.Run();
