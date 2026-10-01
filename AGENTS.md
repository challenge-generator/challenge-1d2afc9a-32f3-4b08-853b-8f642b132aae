# AGENTS.md

Instrucciones para el agente de IA que abra este repositorio (Claude Code, Cursor, Codex, Copilot, Gemini). Se cargan solas: no hay que pegar nada en ningun chat.

## Que es este repositorio

Es el codigo base de un reto de aprendizaje de Pragma: **Diseño de arquitectura de un sistema de gestión de cuentas**.

| | |
|---|---|
| Tema | Diseño de arquitectura |
| Nivel | senior-l1 |
| Chapter | Backend |
| Especialidad | .NET |
| Stack | .NET / ASP.NET Core 9.0 |
| Patron arquitectonico | microservicio reactivo con CQRS y Event Sourcing |
| Tiempo estimado | 1 semana |

## Receta del stack

Esqueleto obligatorio:

- `*.csproj en la raiz del proyecto`
- `Program.cs como entry point`
- `appsettings.json`
- `carpeta Domain con entidades e interfaces`
- `carpeta Application con casos de uso`
- `carpeta Infrastructure con repositorios y Controllers`

Dependencias:

- Microsoft.AspNetCore.App 9.0.0
- Microsoft.NET.Sdk.Web n/a
- MediatR 12.5.0
- MediatR.Extensions.Microsoft.DependencyInjection 12.0.0
- FluentValidation.AspNetCore 12.0.0
- EntityFrameworkCore 9.0.0
- Npgsql.EntityFrameworkCore.PostgreSQL 9.0.0
- Microsoft.AspNetCore.Authentication.JwtBearer 9.0.0
- Swashbuckle.AspNetCore 6.5.0
- Polly 8.0.0
- xunit 2.7.0
- Moq 4.20.0
- Microsoft.NET.Test.Sdk 17.8.0
- coverlet.collector 6.0.0

## Tu tarea

Dejar este proyecto en estado **verificable**: que el comando de verificacion corra sin errores. Escribi los archivos en disco, en este repositorio. No generes ZIPs ni archivos adjuntos.

En orden:

1. Corre `dotnet build` y mira que falla.
2. Completa lo que falte de la lista de abajo: manifiesto de dependencias, punto de entrada, capa de interfaz y las capas del patron declarado.
3. Arregla SOLO los errores que impiden compilar o arrancar.
4. Volve a correr `dotnet build` hasta que pase.
5. Pará ahí.

## Regla dura: las fases son trabajo del humano

**PROHIBIDO implementar los entregables de las fases.** El valor del reto esta en que la persona los resuelva. Tu trabajo es que tenga un proyecto que arranca; el hueco pedagogico se queda como esta.

No resuelvas nada de esto:

- **Fase 1 — Identificación de atributos de calidad**: Documento que describe los atributos de calidad prioritarios y su justificación.
- **Fase 2 — Diseño de la arquitectura**: Documento que describe la arquitectura de software propuesta y su justificación.
- **Fase 3 — Evaluación de la arquitectura**: Documento que evalúa la arquitectura propuesta y propone mejoras si es necesario.

Distincion operativa:

- **Arreglar** (si): import faltante, tipo que no existe, dependencia sin declarar, error de sintaxis, archivo referenciado que no existe.
- **No tocar** (no): logica de negocio incompleta, validaciones ausentes, secretos hardcodeados, APIs deprecadas que funcionan, concurrencia insegura, patrones mejorables. Eso es lo que la persona tiene que encontrar.

## Superficie de practica (NO completes)

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs. No toques la logica que el reto pide completar.

- [ ] `src/AccountManagement.Api/Dockerfile` — El topic pide contenedores/orquestacion: este archivo es el ejercicio, no scaffolding.
- [ ] `infrastructure/docker-compose.yml` — El topic pide contenedores/orquestacion: este archivo es el ejercicio, no scaffolding.

## Lo que falta y tenes que completar

### 1. Referencias colgando (7)

Salieron de un analisis estatico del codigo que SI esta en el repo. Cada una rompe la compilacion:

- [ ] `src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs` — `AccountDbContext.SaveChangesAsync`
      Se invoca `SaveChangesAsync` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs` — `AccountDbContext.Entry`
      Se invoca `Entry` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs` — `AccountDomainEvent.GetType`
      Se invoca `GetType` sobre `AccountDomainEvent`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs` — `AccountDbContext.SaveChangesAsync`
      Se invoca `SaveChangesAsync` sobre `AccountDbContext`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs` — `IAccountRepository.SaveChangesAsync`
      Se invoca `SaveChangesAsync` sobre `IAccountRepository`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs` — `INotificationService.SendAccountCreatedNotificationAsync`
      Se invoca `SendAccountCreatedNotificationAsync` sobre `INotificationService`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.
- [ ] `tests/AccountManagement.IntegrationTests/Controllers/AccountControllerTests.cs` — `AccountController.CloseAccount`
      Se invoca `CloseAccount` sobre `AccountController`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.

### Presentes (25)

- `App.csproj`
- `src/AccountManagement.Api/Program.cs`
- `src/AccountManagement.Api/appsettings.json`
- `src/AccountManagement.Domain/Entities/Account.cs`
- `src/AccountManagement.Domain/Entities/User.cs`
- `src/AccountManagement.Domain/Repositories/IAccountRepository.cs`
- `src/AccountManagement.Infrastructure/Repositories/AccountRepository.cs`
- `src/AccountManagement.Infrastructure/EventStore/AccountEventStore.cs`
- `src/AccountManagement.Infrastructure/Notification/NotificationService.cs`
- `src/AccountManagement.Common/DTOs/AccountDto.cs`
- `src/AccountManagement.Common/Exceptions/AccountException.cs`
- `src/AccountManagement.Common/Exceptions/AccountNotFoundException.cs`
- `src/AccountManagement.Domain/Events/AccountDomainEvent.cs`
- `src/AccountManagement.Domain/Events/AccountCreatedEvent.cs`
- `src/AccountManagement.Domain/Events/AccountUpdatedEvent.cs`
- `src/AccountManagement.Application/Commands/CreateAccountCommand.cs`
- `src/AccountManagement.Application/Commands/UpdateAccountCommand.cs`
- `src/AccountManagement.Application/Queries/GetAccountByIdQuery.cs`
- `src/AccountManagement.Application/Handlers/CreateAccountCommandHandler.cs`
- `src/AccountManagement.Api/Controllers/AccountController.cs`
- `src/AccountManagement.Api/Dockerfile`
- `tests/AccountManagement.UnitTests/Handlers/CreateAccountCommandHandlerTests.cs`
- `tests/AccountManagement.IntegrationTests/Controllers/AccountControllerTests.cs`
- `infrastructure/docker-compose.yml`
- `README.md`

### Capas del patron declarado

Cada una tiene que existir como directorio real con al menos un archivo. Codigo plano en la raiz no satisface el patron.

- `src/AccountManagement.Domain`
- `src/AccountManagement.Application`
- `src/AccountManagement.Infrastructure`
- `src/AccountManagement.Api`
- `src/AccountManagement.Common`
- `tests/AccountManagement.UnitTests`
- `tests/AccountManagement.IntegrationTests`
- `infrastructure`

## Verificacion

```bash
dotnet build
```

El comando tiene que pasar SIN implementar los archivos de la superficie de practica: solo andamiaje.

Ese comando pasando es la definicion de "terminado" para vos.

## Convenciones que tenes que respetar

- Un solo ecosistema: no declares librerias de otro lenguaje ni mezcles gestores de paquetes.
- Toda libreria que uses tiene que estar declarada en el manifiesto de dependencias.
- Todo import declarado tiene que usarse; todo tipo usado tiene que existir o venir de una dependencia declarada.
- El patron es **microservicio reactivo con CQRS y Event Sourcing**: los contratos (interfaces, puertos) los define la capa interna y los implementa la externa, nunca al revés.
- Los archivos que crees llevan implementacion real, no stubs: sin `TODO`, sin cuerpos vacios, sin `// getters y setters`.

## Contexto del candidato

Sirve para calibrar el nivel del codigo, no para resolver las fases.

- Perfil: Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Senior
- Brecha que el reto ataca: Ha trabajado identificando los atributos de calidad más importantes para los requerimientos de una cuenta / proyecto y ha diseñado arquitecturas de software que favorezcan estos atributos de calidad.
- Mision: Candidato Senior con experiencia en Backend y .Net, con conocimiento inicial en Arquitectura.

---

*Generado por Challenge Generator — Pragma. `README.md` tiene el enunciado completo del reto para la persona. `PROMPT_MEJORA.md` es la variante para pegar en un chat, si se prefiere ese flujo.*
