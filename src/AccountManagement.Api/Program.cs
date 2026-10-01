using AccountManagement.Application.Commands;
using AccountManagement.Application.Handlers;
using AccountManagement.Application.Queries;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Events;
using AccountManagement.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Retry;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account Management API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
});

// Configuración de MediatR para CQRS
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(CreateAccountCommand).Assembly,
        typeof(CreateAccountCommandHandler).Assembly
    )
);

// Configuración de FluentValidation
builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CreateAccountCommand>());

// Configuración de Entity Framework Core con PostgreSQL
builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AccountDatabase")));

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Jwt:Authority"];
    options.Audience = builder.Configuration["Jwt:Audience"];
    options.RequireHttpsMetadata = bool.Parse(builder.Configuration["Jwt:RequireHttpsMetadata"] ?? "false");
});

// Configuración de Polly para Circuit Breaker y Retry
var retryPolicy = Policy
    .Handle<Exception>()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

var circuitBreakerPolicy = Policy
    .Handle<Exception>()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

builder.Services.AddSingleton(retryPolicy);
builder.Services.AddSingleton(circuitBreakerPolicy);

// Registro de repositorios y servicios de infraestructura
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountEventStore, AccountEventStore>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Configuración de manejo global de excepciones
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Middleware de manejo global de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Management API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Clase para manejo global de excepciones
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Account not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Account operation failed");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An unexpected error occurred");
        }
    }
}

// Contexto de Entity Framework Core (simplificado para el lote)
public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
}