# Diseño de arquitectura de un sistema de gestión de cuentas

El sistema de gestión de cuentas debe manejar la creación, modificación y eliminación de cuentas de usuarios. Los usuarios pueden ser clientes o empleados. El sistema debe asegurar la consistencia de los datos, la disponibilidad del servicio y la seguridad de las transacciones. Los actores involucrados son el usuario, el sistema de autenticación, el sistema de persistencia y el sistema de notificación. El sistema debe procesar un mínimo de 10 000 transacciones por segundo con una latencia máxima de 100ms. La consistencia se define como la sincronización entre el sistema de persistencia y el sistema de notificación. La disponibilidad se define como la capacidad del sistema para atender solicitudes durante un tiempo de inactividad máximo de 1 hora por mes. La seguridad se define como la protección de los datos de usuarios contra accesos no autorizados.

## Informacion General

| Campo | Valor |
|-------|-------|
| **Tema** | Diseño de arquitectura |
| **Nivel** | senior-l1 |
| **Tipo** | practical |
| **Tiempo estimado** | 1 semana |

## Fases del Reto

### Fase 0: Configuración del Proyecto

**Objetivo:** Obtener el proyecto base funcional enviando el Código Base a un asistente de IA, que lo analizará, corregirá errores y generará un ZIP listo para usar.

**Tiempo estimado:** 15-30 minutos

**Instrucciones:**

- Asegúrate de tener instalado para ejecutar el proyecto: .NET SDK 8+, VS Code o Visual Studio.
- Copia todo el contenido del campo **Código Base** de este reto — incluyendo el texto de instrucciones que aparece al inicio.
- Abre un asistente de IA (Claude en claude.ai, ChatGPT o Gemini — se recomienda Claude), pega el contenido copiado en el chat y envíalo.
- El asistente analizará los archivos, corregirá errores y generará un archivo ZIP descargable. Descárgalo y extráelo en la carpeta donde quieras trabajar.
- Ejecuta `dotnet build`. Si no hay errores, estás listo.

**Entregable:** El proyecto compila/arranca sin errores.

<details>
<summary>Pistas de conocimiento</summary>

- Copia el Código Base completo incluyendo el texto de instrucciones al inicio — esas instrucciones le indican al asistente exactamente qué hacer con los archivos.
- Si el asistente no genera el ZIP automáticamente al terminar el análisis, escríbele: "genera el ZIP ahora".
- Si el proyecto tiene errores al arrancar, comparte el mensaje de error con el mismo asistente para que lo corrija.

</details>

### Fase 1: Identificación de atributos de calidad

**Objetivo:** Identificar los atributos de calidad más importantes para el sistema de gestión de cuentas.

**Tiempo estimado:** 2 días

**Instrucciones:**

- Analiza los requerimientos del sistema y determina los atributos de calidad que deben priorizarse.
- Documenta los atributos de calidad identificados y justifica tu elección.

**Entregable:** Documento que describe los atributos de calidad prioritarios y su justificación.

<details>
<summary>Pistas de conocimiento</summary>

- Considera la consistencia, disponibilidad, seguridad, rendimiento y mantenibilidad como posibles atributos de calidad.

</details>

### Fase 2: Diseño de la arquitectura

**Objetivo:** Diseñar una arquitectura de software que favorezca los atributos de calidad identificados.

**Tiempo estimado:** 3 días

**Instrucciones:**

- Propone una arquitectura de software que cumpla con los atributos de calidad prioritarios.
- Documenta la arquitectura propuesta y justifica tu elección.

**Entregable:** Documento que describe la arquitectura de software propuesta y su justificación.

<details>
<summary>Pistas de conocimiento</summary>

- Considera diferentes patrones de arquitectura, como microservicios, arquitectura orientada a servicios o arquitectura monolítica.

</details>

### Fase 3: Evaluación de la arquitectura

**Objetivo:** Evaluar la arquitectura propuesta y determinar si cumple con los atributos de calidad identificados.

**Tiempo estimado:** 2 días

**Instrucciones:**

- Analiza la arquitectura propuesta y determina si cumple con los atributos de calidad identificados.
- Documenta tus hallazgos y propone mejoras si es necesario.

**Entregable:** Documento que evalúa la arquitectura propuesta y propone mejoras si es necesario.

<details>
<summary>Pistas de conocimiento</summary>

- Considera los pros y contras de la arquitectura propuesta en relación con los atributos de calidad.

</details>

## Dimensiones Evaluadas

- **queEs**: ¿Qué son los atributos de calidad en el contexto del diseño de arquitectura?
- **paraQueSirve**: ¿Para qué sirven los atributos de calidad en el diseño de arquitectura?
- **comoSeUsa**: ¿Cómo se utilizan los atributos de calidad en el diseño de arquitectura?
- **erroresComunes**: ¿Cuáles son los errores comunes al identificar y priorizar los atributos de calidad en el diseño de arquitectura?
- **queDecisionesImplica**: ¿Qué decisiones implica el diseño de una arquitectura que favorezca los atributos de calidad?

## Criterios de Evaluacion

- Identificación correcta de los atributos de calidad más importantes para el sistema de gestión de cuentas.
- Propuesta de una arquitectura de software que favorezca los atributos de calidad identificados.
- Evaluación adecuada de la arquitectura propuesta y propuesta de mejoras si es necesario.

## Como trabajar con un asistente de IA

Hay dos caminos, elegi uno:

- **AGENTS.md** (recomendado) — instrucciones nativas del repo. Abri esta carpeta con tu agente local (Claude Code, Cursor, Codex, Copilot, Gemini) y las carga solo. Sabe que archivos faltan y con que comando se verifica, y completa el scaffold escribiendo en disco.
- **PROMPT_MEJORA.md** — para copiar y pegar en un chat (claude.ai, ChatGPT). Devuelve un ZIP con el proyecto. Sirve si no tenes un agente en el IDE.

Ninguno de los dos resuelve las fases del reto: eso es tu trabajo.

## Verificacion

El proyecto esta listo para trabajar cuando este comando corre sin errores:

```bash
dotnet build
```

---

*Reto generado automaticamente por Challenge Generator - Pragma*
