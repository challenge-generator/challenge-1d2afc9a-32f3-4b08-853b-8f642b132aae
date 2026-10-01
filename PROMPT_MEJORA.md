# Prompt para Mejorar el Codigo Base

Copia y pega el contenido del bloque de abajo en un asistente de IA (Claude, ChatGPT)
para obtener un ZIP con el proyecto completo y arrancable.

Si preferis trabajar en tu editor con un agente local (Claude Code, Cursor, Copilot), usa `AGENTS.md` en vez de este archivo: dice lo mismo pero para que escriba los archivos en disco.

## Las dos reglas que no se negocian

1. **Completa el boilerplate.** Todo lo que el proyecto necesita para compilar y arrancar: manifiesto de dependencias, punto de entrada, configuracion, capa de interfaz, y las capas del patron arquitectonico declarado. Eso es andamiaje y es tu trabajo.
2. **NO resuelvas el reto.** Los entregables de las fases son el trabajo de la persona. El hueco pedagogico se deja como esta: el proyecto arranca, pero lo que el reto pide implementar NO esta implementado.

Dicho de otra forma: si algo impide compilar, arreglalo. Si algo es logica de negocio incompleta, validaciones ausentes, un secreto hardcodeado o un patron mejorable, dejalo exactamente como esta — es lo que la persona tiene que encontrar.

## Superficie de practica — NO resuelvas

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs.

- `src/AccountManagement.Api/Dockerfile` — El topic pide contenedores/orquestacion: este archivo es el ejercicio, no scaffolding.
- `infrastructure/docker-compose.yml` — El topic pide contenedores/orquestacion: este archivo es el ejercicio, no scaffolding.

## Lo que le falta a este proyecto

Esto NO lo tenes que adivinar: salio de comparar el proyecto contra la arquitectura declarada del reto y de un analisis estatico del codigo. Completalo TODO.

### Referencias colgando en el codigo que si esta

Cada una rompe la compilacion:

- `src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs` — `AccountDbContext.SaveChangesAsync`: Se invoca `SaveChangesAsync` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs` — `AccountDbContext.Entry`: Se invoca `Entry` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs` — `AccountDomainEvent.GetType`: Se invoca `GetType` sobre `AccountDomainEvent`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs` — `AccountDbContext.SaveChangesAsync`: Se invoca `SaveChangesAsync` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs` — `IAccountRepository.SaveChangesAsync`: Se invoca `SaveChangesAsync` sobre `IAccountRepository`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs` — `INotificationService.SendAccountCreatedNotificationAsync`: Se invoca `SendAccountCreatedNotificationAsync` sobre `INotificationService`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- `tests/AccountManagement.IntegrationTests/Controllers/AccountControllerTests.cs` — `AccountController.CloseAccount`: Se invoca `CloseAccount` sobre `AccountController`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.

## Como saber que terminaste

```bash
dotnet build
```

Ese comando corriendo sin errores es la definicion de "listo".

---

```
## Briefing del reto (autoridad)
Este bloque manda sobre los archivos adjuntos. El stack y el rol salen de AQUÍ, no de un topic genérico ni de markdown placeholder.

### Perfil
Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Senior

### Brecha de conocimiento
Ha trabajado identificando los atributos de calidad más importantes para los requerimientos de una cuenta / proyecto y ha diseñado arquitecturas de software que favorezcan estos atributos de calidad.

### Misión / candidato
Candidato Senior con experiencia en Backend y .Net, con conocimiento inicial en Arquitectura.

### Reto
- Tema: Diseño de arquitectura
- Seniority: senior-l1
- Tipo: practical
- Título: Diseño de arquitectura de un sistema de gestión de cuentas
- Tiempo estimado: 1 semana

### Fases (trabajo del HUMANO — PROHIBIDO completarlas)
No implementes estos entregables. Dejalos como hueco pedagógico. El asistente solo materializa el proyecto arrancable para que el participante pueda trabajar.
- Fase 1: Identificación de atributos de calidad — objetivo: Identificar los atributos de calidad más importantes para el sistema de gestión de cuentas. — entregable (NO resolver): Documento que describe los atributos de calidad prioritarios y su justificación.
- Fase 2: Diseño de la arquitectura — objetivo: Diseñar una arquitectura de software que favorezca los atributos de calidad identificados. — entregable (NO resolver): Documento que describe la arquitectura de software propuesta y su justificación.
- Fase 3: Evaluación de la arquitectura — objetivo: Evaluar la arquitectura propuesta y determinar si cumple con los atributos de calidad identificados. — entregable (NO resolver): Documento que evalúa la arquitectura propuesta y propone mejoras si es necesario.

Eres un asistente experto en análisis, corrección y generación de archivos de cualquier tipo:
código fuente, documentación, hojas de cálculo, documentos Word, configuraciones, entre otros.
Voy a enviarte una cadena de texto que contiene uno o más archivos. Cada archivo está delimitado por un marcador con el siguiente formato:
// === ARCHIVO: ruta/del/archivo.extension ===
o también puede aparecer como:
## === ARCHIVO: ruta/del/archivo.extension ===
Lo que sigue al marcador puede ser:

El contenido real del archivo (código, texto, YAML, etc.)
Una descripción en lenguaje natural de lo que debe contener el archivo


TU TAREA
PASO 0 — ¿Esto es un proyecto o una carcasa?
Antes de extraer archivos, leé el Briefing (si está) y diagnosticá el adjunto.

Es CARCASA si ocurre CUALQUIERA de estas:
- No hay manifiesto de dependencias del stack del briefing (manifest.json de VTEX IO / package.json / pom.xml / build.gradle / requirements.txt / go.mod / *.tf / *.csproj, según corresponda)
- Hay un "binario" que en realidad es un comentario ("no puede ser mostrado como texto plano", placeholder .fig/.docx vacío)
- Los markdowns ya completan entregables de fases posteriores ("se implementó fade-in", lista de áreas ya resuelta)

Si es CARCASA:
- MATERIALIZÁ un proyecto que arranca en el stack del briefing (VTEX IO Store Framework, Angular, Terraform, pytest, Nest, etc.). Incluí manifiesto, punto de entrada y capa de interfaz reales.
- NO copies los markdowns de "solución" como si fueran el producto. Son ruido de generación.
- NO resuelvas las fases del briefing (están marcadas PROHIBIDO). Dejá el hueco pedagógico: el flujo existe, las microinteracciones/calidad/infra que el reto pide NO están hechas.
- Después seguí al PASO 5 (ZIP).

Si es un proyecto REAL (manifiesto + código que compila o arranca):
- Seguí PASO 1 en adelante. 🔴 compilación sí. 🟡 pedagógico no.

PASO 1 — Detección y extracción
Identifica todos los archivos presentes en la cadena. Para cada archivo extrae:

Su ruta completa (ej: src/main/java/com/pragma/Service.java)
Su contenido o descripción

PASO 2 — Clasificación por tipo
Clasifica cada archivo en una de estas categorías:
A) Código fuente (Java, Python, TypeScript, JavaScript, Kotlin, etc.)
B) Configuración / documentación (YAML, properties, Markdown, JSON, txt, etc.)
C) Excel (.xlsx, .xls, .csv)
D) Word (.docx, .doc)
E) Otro tipo de archivo binario o especial
PASO 3 — Clasificación de errores en código fuente

Objetivo prioritario: que el proyecto compile. No corrijas flujo de negocio ni lógica funcional.

Antes de modificar cualquier archivo de código fuente, clasifica cada problema encontrado en una de estas dos categorías:
🔴 ERROR DE COMPILACIÓN — corregir siempre
Son errores que impiden que el proyecto arranque, sin valor pedagógico:

Import faltante o incorrecto
Clase, método o variable referenciada que no existe en ningún archivo del proyecto
Error de sintaxis
Anotación con atributos inválidos
Dependencia ausente en pom.xml, package.json, etc.
Archivo referenciado que no existe y debe ser creado con implementación mínima

→ CORREGIR estos errores.
🟡 PROBLEMA FUNCIONAL O DE CALIDAD — preservar siempre
Son problemas que no impiden compilar. Pueden ser intencionales para el aprendizaje:

Clave secreta hardcodeada ("secret", "password123")
API deprecada que funciona pero tiene reemplazo moderno
Lógica de negocio incorrecta o incompleta
Código redundante o de baja legibilidad
Falta de validaciones en flujo de negocio
Patrones de diseño incorrectos pero funcionales
Concurrencia no segura
Configuración funcional pero no óptima

→ PRESERVAR tal cual. No corregir, no mejorar, no comentar.
PASO 4 — Procesamiento según tipo de archivo
Tipo A — Código fuente
Aplica únicamente las correcciones clasificadas como 🔴 ERROR DE COMPILACIÓN.
No alteres ningún elemento clasificado como 🟡 PROBLEMA FUNCIONAL O DE CALIDAD.
Si falta un archivo referenciado, créalo con la implementación mínima necesaria para compilar.
Tipo B — Configuración / documentación
Extrae el contenido tal cual, sin modificaciones salvo errores evidentes de sintaxis
(ej: YAML mal indentado).
Tipo C — Excel (.xlsx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un archivo Excel funcional con:

Fila de encabezados en negrita con color de fondo distintivo
Columnas con ancho ajustado al contenido
Tipos de dato correctos por columna
Validaciones si la descripción lo indica
Hojas nombradas descriptivamente si hay más de una
Filas de ejemplo si no hay datos reales

Tipo D — Word (.docx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un documento Word funcional con:

Estilos de título (Título 1, Título 2) para jerarquía de secciones
Fuente legible (Calibri o equivalente), tamaño 11-12pt para cuerpo
Márgenes estándar
Tabla de contenido si tiene múltiples secciones
Tablas con encabezados en negrita si aplica

Tipo E — Otro
Genera el archivo con el contenido o estructura más apropiada según la descripción.
PASO 5 — Exportación en ZIP
Empaqueta todos los archivos en un único archivo ZIP descargable respetando exactamente
la estructura de rutas indicada por los marcadores.
El ZIP debe incluir:

Archivos de código con únicamente los errores de compilación corregidos
Archivos de configuración y documentación sin cambios
Archivos nuevos creados para resolver dependencias de compilación faltantes
Archivos Excel y Word generados desde descripción

IMPORTANTE: El ZIP debe estar listo para descargar al finalizar. No preguntes si el usuario
quiere generarlo. Simplemente genera el archivo y proporciona el enlace de descarga; No debes desplegar en el chat el resumen de lo que arreglaste al Zip, solo entregalo.

REGLAS IMPORTANTES

No omitas ningún archivo aunque no tenga errores ni modificaciones
Respeta los nombres y rutas exactas indicadas por los marcadores
Si un archivo no tiene marcador claro, infiere el nombre desde su contenido
Si la cadena contiene solo documentación, placeholders o binarios fake, NO la reproduzcas:
aplicá PASO 0 (materializar el proyecto del briefing). Reproducir la carcasa es un fallo.
No agregues texto después del enlace de descarga del ZIP
No preguntes si el usuario quiere el ZIP: simplemente generalo siempre
Si detectas que falta un archivo de configuración necesario para compilar
(pom.xml, package.json, requirements.txt, build.gradle, etc.), créalo e inclúyelo
inferiendo su contenido desde los imports y frameworks detectados en el código
Nunca corrijas problemas 🟡 aunque parezcan obvios o fáciles de mejorar.
El participante que recibirá este proyecto los debe encontrar y resolver él mismo.


INPUT
Aquí está la cadena con los archivos:

// === ARCHIVO: App.csproj ===
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <PackageReference Include="Microsoft.AspNetCore.App" Version="9.0.0" />
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
    <PackageReference Include="FluentValidation.AspNetCore" Version="12.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
    <PackageReference Include="Polly" Version="8.0.0" />
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\AccountManagement.Common\AccountManagement.Common.csproj" />
    <ProjectReference Include="..\AccountManagement.Domain\AccountManagement.Domain.csproj" />
    <ProjectReference Include="..\AccountManagement.Application\AccountManagement.Application.csproj" />
    <ProjectReference Include="..\AccountManagement.Infrastructure\AccountManagement.Infrastructure.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="xunit" Version="2.7.0" Condition="'$(Configuration)' == 'Test'" />
    <PackageReference Include="Moq" Version="4.20.0" Condition="'$(Configuration)' == 'Test'" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" Condition="'$(Configuration)' == 'Test'" />
    <PackageReference Include="coverlet.collector" Version="6.0.0" Condition="'$(Configuration)' == 'Test'" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/AccountManagement.Api/Program.cs ===
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

// === ARCHIVO: src/AccountManagement.Api/appsettings.json ===
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "AccountDatabase": "Host=localhost;Port=5432;Database=accountmanagement;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Authority": "https://localhost:5001",
    "Audience": "accountmanagement",
    "RequireHttpsMetadata": "false"
  },
  "NotificationService": {
    "BaseUrl": "http://localhost:5002/api/notifications",
    "RetryCount": 3,
    "CircuitBreaker": {
      "FailureThreshold": 5,
      "DurationOfBreak": 30
    }
  },
  "EventStore": {
    "ConnectionString": "Host=localhost;Port=5432;Database=eventstore;Username=postgres;Password=postgres"
  },
  "Swagger": {
    "Enabled": true
  },
  "FeatureFlags": {
    "UseEventSourcing": true,
    "EnableNotifications": true
  }
}

// === ARCHIVO: src/AccountManagement.Domain/Entities/Account.cs ===
using System;
using System.Collections.Generic;
using AccountManagement.Domain.Events;

namespace AccountManagement.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; }

    private readonly List<AccountDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<AccountDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Account() { }

    public Account(Guid userId, string accountNumber, User user)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty", nameof(userId));
        if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));
        if (user == null) throw new ArgumentNullException(nameof(user));

        Id = Guid.NewGuid();
        UserId = userId;
        AccountNumber = accountNumber;
        Balance = 0m;
        Status = AccountStatus.Active;
        CreatedAt = DateTime.UtcNow;
        User = user;

        _domainEvents.Add(new AccountCreatedEvent(Id, userId, accountNumber, CreatedAt));
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive", nameof(amount));
        if (Status != AccountStatus.Active) throw new InvalidOperationException("Cannot deposit to non-active account");

        Balance += amount;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
        if (Status != AccountStatus.Active) throw new InvalidOperationException("Cannot withdraw from non-active account");
        if (Balance < amount) throw new InvalidOperationException("Insufficient funds");

        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void Close()
    {
        if (Status == AccountStatus.Closed) throw new InvalidOperationException("Account is already closed");

        Status = AccountStatus.Closed;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public enum AccountStatus
{
    Active,
    Closed,
    Suspended
}

// === ARCHIVO: src/AccountManagement.Domain/Entities/User.cs ===
using System;
using System.Collections.Generic;

namespace AccountManagement.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private User() { }

    public User(string name, string email, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty", nameof(email));
        if (!email.Contains("@")) throw new ArgumentException("Invalid email format", nameof(email));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail)) throw new ArgumentException("Email cannot be empty", nameof(newEmail));
        if (!newEmail.Contains("@")) throw new ArgumentException("Invalid email format", nameof(newEmail));

        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRole(UserRole newRole)
    {
        if (Role == newRole) return;

        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum UserRole
{
    Customer,
    Employee,
    Admin
}

// === ARCHIVO: src/AccountManagement.Domain/Repositories/IAccountRepository.cs ===
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Domain.Entities;

namespace AccountManagement.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> AccountNumberExistsAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<bool> UserHasAccountsAsync(Guid userId, CancellationToken cancellationToken = default);
}

// === ARCHIVO: src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs ===
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;
    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(AccountDbContext context, ILogger<AccountRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener una cuenta con ID vacío");
            return null!;
        }

        var account = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (account == null)
        {
            _logger.LogInformation("Cuenta con ID {AccountId} no encontrada", id);
        }
        else
        {
            _logger.LogDebug("Cuenta {AccountId} recuperada exitosamente", id);
        }

        return account!;
    }

    public async Task<Account> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            _logger.LogWarning("Se intentó obtener una cuenta con número de cuenta vacío o nulo");
            return null!;
        }

        var account = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);

        if (account == null)
        {
            _logger.LogInformation("Cuenta con número {AccountNumber} no encontrada", accountNumber);
        }
        else
        {
            _logger.LogDebug("Cuenta {AccountNumber} recuperada exitosamente", accountNumber);
        }

        return account!;
    }

    public async Task<IReadOnlyList<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener cuentas para usuario con ID vacío");
            return Array.Empty<Account>();
        }

        var accounts = await _context.Accounts
            .Include(a => a.User)
            .Where(a => a.UserId == userId)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} cuentas para el usuario {UserId}", accounts.Count, userId);

        return accounts;
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Cuenta {AccountId} con número {AccountNumber} creada exitosamente", 
            account.Id, account.AccountNumber);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        var existingAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == account.Id, cancellationToken);

        if (existingAccount == null)
        {
            _logger.LogError("No se puede actualizar la cuenta {AccountId}: no existe", account.Id);
            throw new InvalidOperationException($"La cuenta con ID {account.Id} no existe");
        }

        _context.Entry(existingAccount).CurrentValues.SetValues(account);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Cuenta {AccountId} actualizada exitosamente", account.Id);
    }

    public async Task<bool> AccountNumberExistsAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            return false;
        }

        var exists = await _context.Accounts
            .AnyAsync(a => a.AccountNumber == accountNumber, cancellationToken);

        _logger.LogDebug("Verificación de número de cuenta {AccountNumber}: {Exists}", accountNumber, exists);

        return exists;
    }

    public async Task<bool> UserHasAccountsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return false;
        }

        var hasAccounts = await _context.Accounts
            .AnyAsync(a => a.UserId == userId, cancellationToken);

        _logger.LogDebug("Usuario {UserId} tiene cuentas: {HasAccounts}", userId, hasAccounts);

        return hasAccounts;
    }
}

// === ARCHIVO: src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs ===
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AccountManagement.Infrastructure.EventStore;

public class AccountEventStore
{
    private readonly AccountDbContext _context;
    private readonly ILogger<AccountEventStore> _logger;

    public AccountEventStore(AccountDbContext context, ILogger<AccountEventStore> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SaveEventAsync(AccountDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        if (domainEvent == null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }

        var storedEvent = new StoredEvent
        {
            Id = Guid.NewGuid(),
            AggregateId = domainEvent.AccountId,
            EventType = domainEvent.GetType().Name,
            EventData = JsonSerializer.Serialize(domainEvent),
            OccurredOn = domainEvent.OccurredOn,
            Version = domainEvent.Version
        };

        await _context.StoredEvents.AddAsync(storedEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Evento {EventType} guardado para aggregate {AggregateId} con versión {Version}",
            storedEvent.EventType, storedEvent.AggregateId, storedEvent.Version);
    }

    public async Task SaveEventsAsync(IEnumerable<AccountDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        if (domainEvents == null)
        {
            throw new ArgumentNullException(nameof(domainEvents));
        }

        var eventsList = domainEvents.ToList();
        if (!eventsList.Any())
        {
            _logger.LogWarning("No hay eventos para guardar");
            return;
        }

        foreach (var domainEvent in eventsList)
        {
            var storedEvent = new StoredEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = domainEvent.AccountId,
                EventType = domainEvent.GetType().Name,
                EventData = JsonSerializer.Serialize(domainEvent),
                OccurredOn = domainEvent.OccurredOn,
                Version = domainEvent.Version
            };

            await _context.StoredEvents.AddAsync(storedEvent, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Se guardaron {Count} eventos para el aggregate", eventsList.Count);
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsForAggregateAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        if (aggregateId == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener eventos para un aggregate con ID vacío");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.OccurredOn)
            .ThenBy(e => e.Version)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos para el aggregate {AggregateId}", events.Count, aggregateId);

        return events;
    }

    public async Task<int> GetEventCountForAggregateAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        if (aggregateId == Guid.Empty)
        {
            return 0;
        }

        var count = await _context.StoredEvents
            .CountAsync(e => e.AggregateId == aggregateId, cancellationToken);

        return count;
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsByTypeAsync(string eventType, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(eventType))
        {
            _logger.LogWarning("Se intentó obtener eventos con tipo vacío o nulo");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.EventType == eventType)
            .OrderByDescending(e => e.OccurredOn)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos de tipo {EventType}", events.Count, eventType);

        return events;
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsInDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        if (startDate > endDate)
        {
            _logger.LogWarning("La fecha de inicio {StartDate} es posterior a la fecha de fin {EndDate}");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.OccurredOn >= startDate && e.OccurredOn <= endDate)
            .OrderBy(e => e.OccurredOn)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos entre {StartDate} y {EndDate}", 
            events.Count, startDate, endDate);

        return events;
    }
}

public class StoredEvent
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public int Version { get; set; }
}

// === ARCHIVO: src/AccountManagement.Infrastructure/Notification/NotificationService.cs ===
using AccountManagement.Domain.Events;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net.Http.Json;
using System.Text.Json;

namespace AccountManagement.Infrastructure.Notification;

public class NotificationService : INotificationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<NotificationService> _logger;
    private readonly ResiliencePipeline<HttpResponseMessage> _resiliencePipeline;
    private readonly NotificationSettings _settings;

    public NotificationService(
        IHttpClientFactory httpClientFactory,
        ILogger<NotificationService> logger,
        NotificationSettings settings)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        _resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = _settings.MaxRetryAttempts,
                Delay = TimeSpan.FromMilliseconds(_settings.RetryDelayMs),
                BackoffType = DelayBackoffType.Exponential,
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TaskCanceledException>()
                    .HandleResult(r => !r.IsSuccessStatusCode && (int)r.StatusCode >= 500)
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
            {
                FailureRatio = _settings.CircuitBreakerFailureRatio,
                SamplingDuration = TimeSpan.FromSeconds(_settings.CircuitBreakerSamplingDurationSeconds),
                MinimumThroughput = _settings.CircuitBreakerMinimumThroughput,
                BreakDuration = TimeSpan.FromSeconds(_settings.CircuitBreakerBreakDurationSeconds),
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TaskCanceledException>()
                    .HandleResult(r => !r.IsSuccessStatusCode)
            })
            .Build();
    }

    public async Task NotifyAccountCreatedAsync(AccountCreatedEvent accountCreatedEvent, CancellationToken cancellationToken = default)
    {
        if (accountCreatedEvent == null)
        {
            throw new ArgumentNullException(nameof(accountCreatedEvent));
        }

        var notification = new AccountNotification
        {
            EventType = "AccountCreated",
            AccountId = accountCreatedEvent.AccountId,
            AccountNumber = accountCreatedEvent.AccountNumber,
            UserId = accountCreatedEvent.UserId,
            Timestamp = accountCreatedEvent.OccurredOn,
            Payload = JsonSerializer.Serialize(accountCreatedEvent)
        };

        await SendNotificationAsync(notification, cancellationToken);
    }

    public async Task NotifyAccountUpdatedAsync(AccountUpdatedEvent accountUpdatedEvent, CancellationToken cancellationToken = default)
    {
        if (accountUpdatedEvent == null)
        {
            throw new ArgumentNullException(nameof(accountUpdatedEvent));
        }

        var notification = new AccountNotification
        {
            EventType = "AccountUpdated",
            AccountId = accountUpdatedEvent.AccountId,
            AccountNumber = accountUpdatedEvent.AccountNumber,
            UserId = accountUpdatedEvent.UserId,
            Timestamp = accountUpdatedEvent.OccurredOn,
            Payload = JsonSerializer.Serialize(accountUpdatedEvent)
        };

        await SendNotificationAsync(notification, cancellationToken);
    }

    private async Task SendNotificationAsync(AccountNotification notification, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_settings.NotificationEndpoint))
        {
            _logger.LogWarning("Endpoint de notificación no configurado. Omitiendo envío.");
            return;
        }

        try
        {
            var client = _httpClientFactory.CreateClient("NotificationClient");
            
            var response = await _resiliencePipeline.ExecuteAsync(
                async ct => await client.PostAsJsonAsync(_settings.NotificationEndpoint, notification, ct),
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Notificación {EventType} enviada exitosamente para cuenta {AccountId}",
                    notification.EventType, notification.AccountId);
            }
            else
            {
                _logger.LogWarning("Envío de notificación {EventType} falló con status {StatusCode}",
                    notification.EventType, response.StatusCode);
            }
        }
        catch (BrokenCircuitException ex)
        {
            _logger.LogError(ex, "Circuit breaker abierto. No se pudo enviar la notificación {EventType}",
                notification.EventType);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar notificación {EventType} para cuenta {AccountId}",
                notification.EventType, notification.AccountId);
            throw;
        }
    }
}

public interface INotificationService
{
    Task NotifyAccountCreatedAsync(AccountCreatedEvent accountCreatedEvent, CancellationToken cancellationToken = default);
    Task NotifyAccountUpdatedAsync(AccountUpdatedEvent accountUpdatedEvent, CancellationToken cancellationToken = default);
}

public class AccountNotification
{
    public string EventType { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Payload { get; set; } = string.Empty;
}

public class NotificationSettings
{
    public string NotificationEndpoint { get; set; } = string.Empty;
    public int MaxRetryAttempts { get; set; } = 3;
    public int RetryDelayMs { get; set; } = 1000;
    public double CircuitBreakerFailureRatio { get; set; } = 0.5;
    public int CircuitBreakerSamplingDurationSeconds { get; set; } = 30;
    public int CircuitBreakerMinimumThroughput { get; set; } = 5;
    public int CircuitBreakerBreakDurationSeconds { get; set; } = 60;
}

// === ARCHIVO: src/AccountManagement.Common/DTOs/AccountDto.cs ===
namespace AccountManagement.Common.DTOs;

public class AccountDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;

    public static AccountDto FromDomain(Domain.Entities.Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));

        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt,
            UserName = account.User?.Name ?? string.Empty,
            UserEmail = account.User?.Email ?? string.Empty,
            UserRole = account.User?.Role.ToString() ?? string.Empty
        };
    }
}

public class CreateAccountDto
{
    public Guid UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}

public class UpdateAccountDto
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public string? Status { get; set; }
}

public class AccountSummaryDto
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
}

// === ARCHIVO: src/AccountManagement.Common/Exceptions/AccountException.cs ===
namespace AccountManagement.Common.Exceptions;

public abstract class AccountException : Exception
{
    public Guid? AccountId { get; }
    public string Code { get; }
    public DateTime OccurredAt { get; }

    protected AccountException(string message, string code, Guid? accountId = null)
        : base(message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        AccountId = accountId;
        OccurredAt = DateTime.UtcNow;
    }

    protected AccountException(string message, string code, Guid? accountId, Exception innerException)
        : base(message, innerException)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        AccountId = accountId;
        OccurredAt = DateTime.UtcNow;
    }

    public virtual Dictionary<string, object> ToErrorDetails()
    {
        var details = new Dictionary<string, object>
        {
            { "type", "https://tools.ietf.org/html/rfc7231#section-6.6.1" },
            { "title", "Account Error" },
            { "status", 500 },
            { "code", Code },
            { "message", Message },
            { "occurredAt", OccurredAt }
        };

        if (AccountId.HasValue)
        {
            details["accountId"] = AccountId.Value;
        }

        return details;
    }
}

public enum AccountErrorCode
{
    Unknown = 0,
    AccountNotFound = 1001,
    AccountAlreadyExists = 1002,
    InvalidAccountNumber = 1003,
    InsufficientFunds = 1004,
    AccountClosed = 1005,
    AccountLocked = 1006,
    InvalidAmount = 1007,
    UserNotFound = 1008,
    ValidationError = 1009,
    ConcurrencyConflict = 1010,
    ExternalServiceFailure = 1011
}

// === ARCHIVO: src/AccountManagement.Common/Exceptions/AccountNotFoundException.cs ===
namespace AccountManagement.Common.Exceptions;

public class AccountNotFoundException : AccountException
{
    public AccountNotFoundException(Guid accountId)
        : base(
            $"La cuenta con ID '{accountId}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            accountId)
    {
    }

    public AccountNotFoundException(string accountNumber)
        : base(
            $"La cuenta con número '{accountNumber}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            null)
    {
    }

    public AccountNotFoundException(Guid accountId, Exception innerException)
        : base(
            $"La cuenta con ID '{accountId}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            accountId,
            innerException)
    {
    }

    public override Dictionary<string, object> ToErrorDetails()
    {
        var details = base.ToErrorDetails();
        details["title"] = "Account Not Found";
        details["status"] = 404;
        details["code"] = AccountErrorCode.AccountNotFound.ToAccountErrorString();
        return details;
    }
}

public static class AccountErrorCodeExtensions
{
    public static string ToAccountErrorString(this AccountErrorCode code)
    {
        return code switch
        {
            AccountErrorCode.Unknown => "UNKNOWN_ERROR",
            AccountErrorCode.AccountNotFound => "ACCOUNT_NOT_FOUND",
            AccountErrorCode.AccountAlreadyExists => "ACCOUNT_ALREADY_EXISTS",
            AccountErrorCode.InvalidAccountNumber => "INVALID_ACCOUNT_NUMBER",
            AccountErrorCode.InsufficientFunds => "INSUFFICIENT_FUNDS",
            AccountErrorCode.AccountClosed => "ACCOUNT_CLOSED",
            AccountErrorCode.AccountLocked => "ACCOUNT_LOCKED",
            AccountErrorCode.InvalidAmount => "INVALID_AMOUNT",
            AccountErrorCode.UserNotFound => "USER_NOT_FOUND",
            AccountErrorCode.ValidationError => "VALIDATION_ERROR",
            AccountErrorCode.ConcurrencyConflict => "CONCURRENCY_CONFLICT",
            AccountErrorCode.ExternalServiceFailure => "EXTERNAL_SERVICE_FAILURE",
            _ => "UNKNOWN_ERROR"
        };
    }
}

// === ARCHIVO: src/AccountManagement.Domain/Events/AccountDomainEvent.cs ===
namespace AccountManagement.Domain.Events;

/// <summary>
/// Base class for all domain events in the account management system.
/// Implements the event sourcing pattern for immutable event storage.
/// </summary>
public abstract class AccountDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredAt { get; }
    public string EventType { get; }
    public Guid AggregateId { get; }
    public int Version { get; protected set; }
    public Dictionary<string, object> Metadata { get; }

    protected AccountDomainEvent(Guid aggregateId, string eventType)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("AggregateId cannot be empty", nameof(aggregateId));

        Id = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
        AggregateId = aggregateId;
        Version = 1;
        Metadata = new Dictionary<string, object>();
    }

    public void IncrementVersion() => Version++;

    public void AddMetadata(string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Metadata key cannot be empty", nameof(key));

        Metadata[key] = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool TryGetMetadata<T>(string key, out T? value)
    {
        if (Metadata.TryGetValue(key, out var rawValue) && rawValue is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }

    public override string ToString() => $"{EventType} (AggregateId: {AggregateId}, Version: {Version}, OccurredAt: {OccurredAt:yyyy-MM-dd HH:mm:ss})";
}

// === ARCHIVO: src/AccountManagement.Domain/Events/AccountCreatedEvent.cs ===
namespace AccountManagement.Domain.Events;

/// <summary>
/// Domain event raised when a new account is created in the system.
/// Contains all necessary information about the newly created account for event sourcing and notification purposes.
/// </summary>
public class AccountCreatedEvent : AccountDomainEvent
{
    public Guid UserId { get; }
    public string AccountNumber { get; }
    public decimal InitialBalance { get; }
    public string AccountType { get; }
    public string Currency { get; }
    public DateTime CreatedAt { get; }
    public string CreatedBy { get; }

    public AccountCreatedEvent(
        Guid aggregateId,
        Guid userId,
        string accountNumber,
        decimal initialBalance,
        string accountType,
        string currency,
        string createdBy) : base(aggregateId, nameof(AccountCreatedEvent))
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(userId));

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("AccountNumber cannot be empty", nameof(accountNumber));

        if (initialBalance < 0)
            throw new ArgumentException("InitialBalance cannot be negative", nameof(initialBalance));

        if (string.IsNullOrWhiteSpace(accountType))
            throw new ArgumentException("AccountType cannot be empty", nameof(accountType));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("CreatedBy cannot be empty", nameof(createdBy));

        UserId = userId;
        AccountNumber = accountNumber;
        InitialBalance = initialBalance;
        AccountType = accountType;
        Currency = currency;
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;

        AddMetadata("InitialBalanceFormatted", $"{initialBalance:N2} {currency}");
        AddMetadata("UserId", userId.ToString());
        AddMetadata("AccountType", accountType);
    }

    public override string ToString() =>
        $"AccountCreated: AccountNumber={AccountNumber}, UserId={UserId}, InitialBalance={InitialBalance} {Currency}, Type={AccountType}, CreatedBy={CreatedBy}";
}

// === ARCHIVO: src/AccountManagement.Domain/Events/AccountUpdatedEvent.cs ===
namespace AccountManagement.Domain.Events;

/// <summary>
/// Domain event raised when an account is updated in the system.
/// Captures the state changes made to an existing account for event sourcing and audit purposes.
/// </summary>
public class AccountUpdatedEvent : AccountDomainEvent
{
    public string? PreviousAccountNumber { get; }
    public string? NewAccountNumber { get; }
    public decimal? PreviousBalance { get; }
    public decimal? NewBalance { get; }
    public string? PreviousStatus { get; }
    public string? NewStatus { get; }
    public DateTime UpdatedAt { get; }
    public string UpdatedBy { get; }
    public string ChangeReason { get; }
    public List<string> ChangedFields { get; }

    public AccountUpdatedEvent(
        Guid aggregateId,
        string updatedBy,
        string changeReason) : base(aggregateId, nameof(AccountUpdatedEvent))
    {
        if (string.IsNullOrWhiteSpace(updatedBy))
            throw new ArgumentException("UpdatedBy cannot be empty", nameof(updatedBy));

        if (string.IsNullOrWhiteSpace(changeReason))
            throw new ArgumentException("ChangeReason cannot be empty", nameof(changeReason));

        UpdatedBy = updatedBy;
        ChangeReason = changeReason;
        UpdatedAt = DateTime.UtcNow;
        ChangedFields = new List<string>();

        AddMetadata("ChangeReason", changeReason);
        AddMetadata("UpdatedBy", updatedBy);
    }

    public void RecordAccountNumberChange(string previousNumber, string newNumber)
    {
        if (string.IsNullOrWhiteSpace(previousNumber))
            throw new ArgumentException("PreviousAccountNumber cannot be empty", nameof(previousNumber));

        if (string.IsNullOrWhiteSpace(newNumber))
            throw new ArgumentException("NewAccountNumber cannot be empty", nameof(newNumber));

        PreviousAccountNumber = previousNumber;
        NewAccountNumber = newNumber;
        ChangedFields.Add("AccountNumber");
        AddMetadata("PreviousAccountNumber", previousNumber);
        AddMetadata("NewAccountNumber", newNumber);
    }

    public void RecordBalanceChange(decimal previousBalance, decimal newBalance)
    {
        if (previousBalance < 0)
            throw new ArgumentException("PreviousBalance cannot be negative", nameof(previousBalance));

        if (newBalance < 0)
            throw new ArgumentException("NewBalance cannot be negative", nameof(newBalance));

        PreviousBalance = previousBalance;
        NewBalance = newBalance;
        ChangedFields.Add("Balance");
        AddMetadata("PreviousBalance", previousBalance);
        AddMetadata("NewBalance", newBalance);
        AddMetadata("BalanceDifference", newBalance - previousBalance);
    }

    public void RecordStatusChange(string previousStatus, string newStatus)
    {
        if (string.IsNullOrWhiteSpace(previousStatus))
            throw new ArgumentException("PreviousStatus cannot be empty", nameof(previousStatus));

        if (string.IsNullOrWhiteSpace(newStatus))
            throw new ArgumentException("NewStatus cannot be empty", nameof(newStatus));

        PreviousStatus = previousStatus;
        NewStatus = newStatus;
        ChangedFields.Add("Status");
        AddMetadata("PreviousStatus", previousStatus);
        AddMetadata("NewStatus", newStatus);
    }

    public bool HasChanges() => ChangedFields.Count > 0;

    public override string ToString() =>
        $"AccountUpdated: AggregateId={AggregateId}, ChangedFields=[{string.Join(", ", ChangedFields)}], UpdatedBy={UpdatedBy}, Reason={ChangeReason}";
}

// === ARCHIVO: src/AccountManagement.Application/Commands/CreateAccountCommand.cs ===
using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Commands;

public record CreateAccountCommand(
    Guid UserId,
    string AccountNumber,
    string AccountType,
    decimal InitialBalance
) : IRequest<AccountDto>;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("El identificador de usuario es requerido");

        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage("El número de cuenta es requerido")
            .Matches(@"^[0-9]{10,20}$")
            .WithMessage("El número de cuenta debe contener solo dígitos y tener entre 10 y 20 caracteres");

        RuleFor(x => x.AccountType)
            .NotEmpty()
            .WithMessage("El tipo de cuenta es requerido")
            .Must(BeValidAccountType)
            .WithMessage("El tipo de cuenta debe ser: CuentaCorriente, CuentaAhorros o CuentaInversion");

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El saldo inicial no puede ser negativo")
            .When(x => x.InitialBalance > 0)
            .LessThanOrEqualTo(1000000m)
            .WithMessage("El saldo inicial no puede exceder 1,000,000");
    }

    private bool BeValidAccountType(string accountType)
    {
        var validTypes = new[] { "CuentaCorriente", "CuentaAhorros", "CuentaInversion" };
        return validTypes.Contains(accountType);
    }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<CreateAccountCommandHandler> _logger;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        ILogger<CreateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación de cuenta para usuario {UserId}", request.UserId);

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.AccountNumber, 
            cancellationToken);

        if (accountNumberExists)
        {
            _logger.LogWarning("El número de cuenta {AccountNumber} ya existe", request.AccountNumber);
            throw new AccountException($"El número de cuenta {request.AccountNumber} ya está en uso");
        }

        var account = new Account(
            request.UserId,
            request.AccountNumber,
            null);

        if (request.InitialBalance > 0)
        {
            account.Deposit(request.InitialBalance);
        }

        await _accountRepository.AddAsync(account, cancellationToken);

        _logger.LogInformation("Cuenta {AccountId} creada exitosamente para usuario {UserId}", 
            account.Id, 
            request.UserId);

        return MapToDto(account);
    }

    private static AccountDto MapToDto(Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }
}

// === ARCHIVO: src/AccountManagement.Application/Commands/UpdateAccountCommand.cs ===
using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Commands;

public record UpdateAccountCommand(
    Guid AccountId,
    string NewAccountNumber,
    string NewStatus
) : IRequest<AccountDto>;

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("El identificador de cuenta es requerido");

        RuleFor(x => x.NewAccountNumber)
            .NotEmpty()
            .WithMessage("El número de cuenta es requerido")
            .Matches(@"^[0-9]{10,20}$")
            .WithMessage("El número de cuenta debe contener solo dígitos y tener entre 10 y 20 caracteres");

        RuleFor(x => x.NewStatus)
            .NotEmpty()
            .WithMessage("El estado de cuenta es requerido")
            .Must(BeValidStatus)
            .WithMessage("El estado debe ser: Activa, Inactiva o Cerrada");
    }

    private bool BeValidStatus(string status)
    {
        var validStatuses = new[] { "Activa", "Inactiva", "Cerrada" };
        return validStatuses.Contains(status);
    }
}

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<UpdateAccountCommandHandler> _logger;

    public UpdateAccountCommandHandler(
        IAccountRepository accountRepository,
        ILogger<UpdateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando actualización de cuenta {AccountId}", request.AccountId);

        var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Cuenta {AccountId} no encontrada", request.AccountId);
            throw new AccountNotFoundException($"La cuenta con ID {request.AccountId} no fue encontrada");
        }

        if (account.Status == AccountStatus.Cerrada)
        {
            _logger.LogWarning("No se puede modificar la cuenta {AccountId} porque está cerrada", request.AccountId);
            throw new AccountException("No se puede modificar una cuenta que está cerrada");
        }

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.NewAccountNumber, 
            cancellationToken);

        if (accountNumberExists && request.NewAccountNumber != account.AccountNumber)
        {
            _logger.LogWarning("El número de cuenta {AccountNumber} ya está en uso", request.NewAccountNumber);
            throw new AccountException($"El número de cuenta {request.NewAccountNumber} ya está en uso");
        }

        if (Enum.TryParse<AccountStatus>(request.NewStatus, out var newStatus))
        {
            if (newStatus == AccountStatus.Cerrada)
            {
                account.Close();
            }
        }

        await _accountRepository.UpdateAsync(account, cancellationToken);

        _logger.LogInformation("Cuenta {AccountId} actualizada exitosamente", request.AccountId);

        return MapToDto(account);
    }

    private static AccountDto MapToDto(Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }
}

// === ARCHIVO: src/AccountManagement.Application/Queries/GetAccountByIdQuery.cs ===
using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Queries;

public record GetAccountByIdQuery(Guid AccountId) : IRequest<AccountDto>;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("El identificador de cuenta es requerido");
    }
}

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<GetAccountByIdQueryHandler> _logger;

    public GetAccountByIdQueryHandler(
        IAccountRepository accountRepository,
        ILogger<GetAccountByIdQueryHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consultando cuenta {AccountId}", request.AccountId);

        var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Cuenta {AccountId} no encontrada", request.AccountId);
            throw new AccountNotFoundException($"La cuenta con ID {request.AccountId} no fue encontrada");
        }

        _logger.LogInformation("Cuenta {AccountId} encontrada exitosamente", request.AccountId);

        return MapToDto(account);
    }

    private static AccountDto MapToDto(AccountManagement.Domain.Entities.Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }
}

// === ARCHIVO: src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs ===
using MediatR;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using AccountManagement.Application.Commands;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Events;

namespace AccountManagement.Application.Handlers;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly IAccountEventStore _eventStore;
    private readonly ILogger<CreateAccountCommandHandler> _logger;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        INotificationService notificationService,
        IAccountEventStore eventStore,
        ILogger<CreateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación de cuenta para usuario {UserId}", request.UserId);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Usuario no encontrado: {UserId}", request.UserId);
            throw new AccountNotFoundException($"Usuario con ID {request.UserId} no encontrado");
        }

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.AccountNumber, cancellationToken);
        if (accountNumberExists)
        {
            _logger.LogWarning("Número de cuenta ya existe: {AccountNumber}", request.AccountNumber);
            throw new AccountException($"El número de cuenta {request.AccountNumber} ya existe");
        }

        var account = new Account(
            userId: request.UserId,
            accountNumber: request.AccountNumber,
            user: user);

        if (request.InitialBalance > 0)
        {
            account.Deposit(request.InitialBalance);
        }

        await _accountRepository.AddAsync(account, cancellationToken);
        await _accountRepository.SaveChangesAsync(cancellationToken);

        var accountCreatedEvent = new AccountCreatedEvent(
            accountId: account.Id,
            accountNumber: account.AccountNumber,
            userId: account.UserId,
            initialBalance: request.InitialBalance,
            timestamp: DateTime.UtcNow);

        await _eventStore.SaveEventAsync(accountCreatedEvent, cancellationToken);

        await _notificationService.SendAccountCreatedNotificationAsync(
            account.Id,
            account.AccountNumber,
            user.Email,
            cancellationToken);

        _logger.LogInformation("Cuenta creada exitosamente: {AccountId}", account.Id);

        return new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            UserId = account.UserId,
            Balance = account.Balance,
            Status = account.Status,
            CreatedAt = account.CreatedAt,
            UserName = user.Name,
            UserEmail = user.Email
        };
    }
}

// === ARCHIVO: src/AccountManagement.Api/Controllers/AccountController.cs ===
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Queries;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using System.Security.Claims;

namespace AccountManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Recibida solicitud para crear cuenta: {AccountNumber}", 
                command.AccountNumber);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Token de autenticación inválido o ausente"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta creada exitosamente: {AccountId}", result.Id);
            return CreatedAtAction(nameof(GetAccountById), new { id = result.Id }, result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Recurso no encontrado: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al crear cuenta");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Consultando cuenta: {AccountId}", id);

            var query = new GetAccountByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                _logger.LogWarning("Cuenta no encontrada: {AccountId}", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = $"Cuenta con ID {id} no encontrada"
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountsByUserId(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Consultando cuentas para usuario: {UserId}", userId);

            var query = new GetAccountsByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar cuentas de usuario: {UserId}", userId);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAccount(
        Guid id,
        [FromBody] UpdateAccountCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Actualizando cuenta: {AccountId}", id);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta actualizada exitosamente: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para actualizar: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio al actualizar cuenta: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al actualizar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Eliminando cuenta: {AccountId}", id);

            var command = new DeleteAccountCommand(id);
            await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta eliminada exitosamente: {AccountId}", id);
            return NoContent();
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para eliminar: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio al eliminar cuenta: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al eliminar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPost("{id:guid}/deposit")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deposit(
        Guid id,
        [FromBody] DepositCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Depósito en cuenta: {AccountId}, monto: {Amount}", id, command.Amount);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Depósito exitoso en cuenta: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para depósito: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio en depósito: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno en depósito: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPost("{id:guid}/withdraw")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Withdraw(
        Guid id,
        [FromBody] WithdrawCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retiro de cuenta: {AccountId}, monto: {Amount}", id, command.Amount);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Retiro exitoso de cuenta: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para retiro: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio en retiro: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno en retiro: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }
}

// === ARCHIVO: src/AccountManagement.Api/Dockerfile ===
# STUB: Superficie de práctica - el estudiante implementa el Dockerfile completo
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AccountManagement.Api/AccountManagement.Api.csproj", "AccountManagement.Api/"]
COPY ["AccountManagement.Application/AccountManagement.Application.csproj", "AccountManagement.Application/"]
COPY ["AccountManagement.Domain/AccountManagement.Domain.csproj", "AccountManagement.Domain/"]
COPY ["AccountManagement.Infrastructure/AccountManagement.Infrastructure.csproj", "AccountManagement.Infrastructure/"]
COPY ["AccountManagement.Common/AccountManagement.Common.csproj", "AccountManagement.Common/"]
RUN dotnet restore "AccountManagement.Api/AccountManagement.Api.csproj"
COPY . .
WORKDIR "/src/AccountManagement.Api"
RUN dotnet build "AccountManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountManagement.Api.dll"]

// === ARCHIVO: tests/AccountManagement.UnitTests/Handlers/CreateAccountCommandHandlerTests.cs ===
using Xunit;
using Moq;
using MediatR;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Handlers;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using AccountManagement.Common.Exceptions;

namespace AccountManagement.UnitTests.Handlers;

public class CreateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateAccountCommandHandler _handler;

    public CreateAccountCommandHandlerTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateAccountCommandHandler(
            _accountRepositoryMock.Object,
            _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAccountSuccessfully()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-001",
            InitialDeposit = 1000.00m
        };

        var user = new User("Test User", "test@example.com", UserRole.Client);
        
        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.AccountNumber, result.AccountNumber);
        Assert.Equal(command.InitialDeposit, result.Balance);
        _accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateAccountNumber_ThrowsAccountException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-DUPLICATE",
            InitialDeposit = 500.00m
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<AccountException>(() => _handler.Handle(command, CancellationToken.None));
        
        _accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(-100.00)]
    [InlineData(-0.01)]
    [InlineData(-1000.50)]
    public async Task Handle_NegativeInitialDeposit_ThrowsArgumentException(decimal invalidAmount)
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-002",
            InitialDeposit = invalidAmount
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidCommand_PublishesAccountCreatedEvent()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-EVENT",
            InitialDeposit = 2500.00m
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mediatorMock.Verify(
            x => x.Publish(
                It.Is<AccountCreatedEvent>(e => e.AccountNumber == command.AccountNumber),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyAccountNumber_ThrowsArgumentException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = string.Empty,
            InitialDeposit = 100.00m
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmptyUserId_ThrowsArgumentException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.Empty,
            AccountNumber = "ACC-2024-003",
            InitialDeposit = 100.00m
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Theory]
    [InlineData(0.00)]
    [InlineData(0.01)]
    [InlineData(999999.99)]
    public async Task Handle_ValidAmounts_DoesNotThrow(decimal validAmount)
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-VALID",
            InitialDeposit = validAmount
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.NotNull(result);
    }
}

// === ARCHIVO: tests/AccountManagement.IntegrationTests/Controllers/AccountControllerTests.cs ===
using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AccountManagement.Api.Controllers;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Queries;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;

namespace AccountManagement.IntegrationTests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<AccountController>> _loggerMock;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AccountController>>();
        _controller = new AccountController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAccount_ValidRequest_ReturnsCreatedResult()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-TEST-001",
            InitialDeposit = 1000.00m
        };

        var expectedDto = new AccountDto
        {
            Id = Guid.NewGuid(),
            AccountNumber = request.AccountNumber,
            Balance = request.InitialDeposit,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(createdResult);
        var returnValue = Assert.IsType<AccountDto>(createdResult.Value);
        Assert.Equal(expectedDto.AccountNumber, returnValue.AccountNumber);
    }

    [Fact]
    public async Task CreateAccount_RepositoryThrowsException_ReturnsBadRequest()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-DUPLICATE",
            InitialDeposit = 500.00m
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AccountException("Account number already exists"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);
    }

    [Fact]
    public async Task GetAccountById_ExistingId_ReturnsOkResult()
    {
        var accountId = Guid.NewGuid();
        var query = new GetAccountByIdQuery { Id = accountId };
        
        var expectedDto = new AccountDto
        {
            Id = accountId,
            AccountNumber = "ACC-GET-001",
            Balance = 2500.00m,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.GetAccountById(accountId, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
        var returnValue = Assert.IsType<AccountDto>(okResult.Value);
        Assert.Equal(accountId, returnValue.Id);
    }

    [Fact]
    public async Task GetAccountById_NonExistingId_ReturnsNotFound()
    {
        var accountId = Guid.NewGuid();
        var query = new GetAccountByIdQuery { Id = accountId };

        _mediatorMock
            .Setup(x => x.Send(query, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AccountNotFoundException(accountId));

        var result = await _controller.GetAccountById(accountId, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateAccount_ValidRequest_ReturnsOkResult()
    {
        var accountId = Guid.NewGuid();
        var request = new UpdateAccountCommand
        {
            Id = accountId,
            NewBalance = 5000.00m
        };

        var expectedDto = new AccountDto
        {
            Id = accountId,
            AccountNumber = "ACC-UPDATE-001",
            Balance = request.NewBalance,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.UpdateAccount(accountId, request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
        var returnValue = Assert.IsType<AccountDto>(okResult.Value);
        Assert.Equal(expectedDto.Balance, returnValue.Balance);
    }

    [Fact]
    public async Task GetAccountById_InvalidId_ReturnsBadRequest()
    {
        var invalidId = Guid.Empty;

        var result = await _controller.GetAccountById(invalidId, CancellationToken.None);

        Assert.IsType<BadRequestResult>(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task CreateAccount_InvalidInitialDeposit_ReturnsBadRequest(decimal invalidAmount)
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-INVALID",
            InitialDeposit = invalidAmount
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException("Initial deposit must be positive"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteAccount_ExistingId_ReturnsNoContent()
    {
        var accountId = Guid.NewGuid();
        var request = new UpdateAccountCommand
        {
            Id = accountId,
            NewBalance = 0
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AccountDto { Id = accountId, Status = AccountStatus.Closed });

        var result = await _controller.CloseAccount(accountId, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task CreateAccount_MediatorThrowsUnhandledException_Returns500()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-ERROR",
            InitialDeposit = 100.00m
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
    }
}

// === ARCHIVO: infrastructure/docker-compose.yml ===
version: '3.8'

services:
  postgres:
    image: postgres:16-alpine
    container_name: accountmanagement_postgres
    environment:
      POSTGRES_USER: accountuser
      POSTGRES_PASSWORD: accountpass123
      POSTGRES_DB: accountmanagement
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U accountuser -d accountmanagement"]
      interval: 10s
      timeout: 5s
      retries: 5

  redis:
    image: redis:7-alpine
    container_name: accountmanagement_redis
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data
    command: redis-server --appendonly yes
    healthcheck:
      test: ["CMD", "redis-cli", "ping"]
      interval: 10s
      timeout: 3s
      retries: 3

  rabbitmq:
    image: rabbitmq:3-management-alpine
    container_name: accountmanagement_rabbitmq
    environment:
      RABBITMQ_DEFAULT_USER: guest
      RABBITMQ_DEFAULT_PASS: guest
    ports:
      - "5672:5672"
      - "15672:15672"
    volumes:
      - rabbitmq_data:/var/lib/rabbitmq
    healthcheck:
      test: ["CMD", "rabbitmq-diagnostics", "ping"]
      interval: 10s
      timeout: 5s
      retries: 5

volumes:
  postgres_data:
  redis_data:
  rabbitmq_data:

// === ARCHIVO: README.md ===
# Account Management API

Sistema de gestión de cuentas desarrollado con ASP.NET Core 9.0 siguiendo una arquitectura de microservicio reactivo con CQRS y Event Sourcing.

## Requisitos Previos

- .NET 9 SDK
- PostgreSQL 15+ (o Docker)
- Visual Studio 2022, Rider o VS Code

## Estructura del Proyecto

```
AccountManagement/
├── src/
│   ├── AccountManagement.Api/           # Punto de entrada REST
│   ├── AccountManagement.Application/   # Casos de uso, comandos y queries
│   ├── AccountManagement.Domain/        # Entidades y reglas de negocio
│   ├── AccountManagement.Infrastructure/# Repositorios y servicios externos
│   └── AccountManagement.Common/        # DTOs y excepciones compartidas
├── tests/
│   ├── AccountManagement.UnitTests/     # Pruebas unitarias
│   └── AccountManagement.IntegrationTests/ # Pruebas de integración
└── infrastructure/
    └── docker-compose.yml               # Orquestación de servicios
```

## Configuración Local

### Opción 1: Con Docker Compose

```bash
cd infrastructure
docker-compose up -d
```

Esto levanta PostgreSQL ypgAdmin automáticamente.

### Opción 2: Base de datos local

Crear una base de datos PostgreSQL llamada `account_management` y actualizar la connection string en `src/AccountManagement.Api/appsettings.json`.

## Ejecución del Proyecto

```bash
# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar (desde src/AccountManagement.Api)
dotnet run
```

La API estará disponible en `https://localhost:7000` (puerto configurable en appsettings).

## Documentación de la API

Una vez ejecutando, acceder a:
- Swagger UI: `https://localhost:7000/swagger`
- OpenAPI JSON: `https://localhost:7000/swagger/v1/swagger.json`

## Testing

```bash
# Ejecutar pruebas unitarias
dotnet test --configuration Test

# Con cobertura
dotnet test --configuration Test --collect:"XPlat Code Coverage"
```

## Tecnologías Utilizadas

- **Framework**: ASP.NET Core 9.0
- **Patrón**: CQRS con MediatR
- **Persistencia**: Entity Framework Core + PostgreSQL
- **Eventos**: Event Sourcing
- **Resiliencia**: Polly (Circuit Breaker, Retry)
- **Autenticación**: JWT Bearer
- **Documentación**: Swashbuckle/OpenAPI

## Capas del Proyecto

| Capa | Responsabilidad |
|------|-----------------|
| Domain | Entidades, eventos de dominio, interfaces de repositorio |
| Application | Comandos, queries, handlers, validación |
| Infrastructure | Implementación de repositorios, servicios externos |
| Api | Controllers, middleware, configuración HTTP |

## Convenciones de Código

- Dominio sin dependencias externas (solo .NET estándar)
- Interfaces de repositorio definidas en Domain, implementadas en Infrastructure
- MediatR para mediación entre Application y capas inferiores
- FluentValidation para validación de modelos
- DTOs en Common para evitar fugas de entidades de dominio
```
