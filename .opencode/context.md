# Project Context

## Environment
- Language: C# / .NET Framework (Web API 2 + ASP.NET MVC 5)
- Runtime: .NET Framework (full), hosted on IIS
- Build: MSBuild / Visual Studio solution (`.sln`) — two web projects
- Test: No test project detected (no unit/integration tests present)
- Package Manager: NuGet (`packages.config` per project)
- Data Access: SQL Server via stored procedures (no ORM)

## Project Type
- [x] Application (Web): back-office para administrar un despacho jurídico
- Compuesta por 2 aplicaciones + bibliotecas de dominio por módulo (arquitectura por capas)

## Solution Layout
### Applications (entry points)
- `DespachoJuridicoDESIMVC` — ASP.NET MVC 5 (vistas Razor, Bootstrap 3, DataTables, FullCalendar). Consume la Web API vía `HttpClientConnection`.
- `DespachoJuridicoDESIWebApi` — ASP.NET Web API 2 (OAuth Bearer, Swagger, Unity DI).

### Layers per module (repeated pattern)
For each domain module: `X.Domain` (entities/objects), `X.Application` (app services + interfaces), `X.Proxy` (SP wrappers + mappers), `X.Messages` (request/response DTOs).
- Modules present: `Agenda`, `Case` (+ `StatusCase`), `Catalog`, `Common.Domain`, `Court`, `Customer`, `Dashboard`, `EventType`, `Record`, `User`.
- Shared data-access: `SqlProxy` (`BaseDbWrapper`, `DbWrapper`).
- Note: `Agenda.Messages` folder is misspelled `Agenda.Messagess`; `Court.Messages` is `CourtMessages`.

## Infrastructure
- Container: None
- Orchestration: None
- CI/CD: None detected
- Cloud: None

## Structure
- Source (MVC): `DespachoJuridicoDESIMVC/` (Controllers, Views, Models, DAL, Helpers, CSS)
- Source (API): `DespachoJuridicoDESIWebApi/` (Controllers, App_Start: Startup.cs, WebApiConfig.cs, UnityConfig.cs, SwaggerConfig.cs)
- Layer libs: `<Module>.Domain`, `<Module>.Application`, `<Module>.Proxy`, `<Module>.Messages`
- Tests: None
- Docs: `README.md` (minimal)
- Entry (API): `Global.asax.cs` → `WebApiConfig.Register`; OWIN `Startup.cs`
- Entry (MVC): `Global.asax` / `App_Start`

## Existing Modules (functionality inventory)
- Clientes: `ClientObj` (Nombre, Telefono, Correo) — CRUD + listado (MVC + API `api/Customer`).
- Casos: `CaseObj` (Cliente, EstatusCaso, NumeroCaso, Descripcion, MontoInicial) — CRUD + listado (`api/Case`).
- Estatus de Caso: `StatusCaseObj` (Nombre, Descripcion, Orden, GeneraEvento, GeneraNotificacion) — catálogo.
- Expedientes: `RecordObj` (Caso, Juzgado, RecordNumber/NumeroExpediente, Comentarios) — CRUD + listado (`api/Record`).
- Juzgados: `CourtObj` (Nombre, Direccion, Colonia, Ciudad, Estado, CP, Telefono, Horario, Observaciones) — CRUD (`api/Court`).
- Agenda: `AgendaObj` (Caso, TipoEvento, Titulo, Descripcion, FechaInicio/Fin, GoogleEventId, SincronizadoGoogle, FechaSincronizacion) — CRUD (`api/Agenda`); campos Google SIN integración real.
- Tipos de Evento: `EventTypeObj` (Nombre, Descripcion) — catálogo (`api/EventType`).
- Dashboard: `DashboardObj` (SummaryCards, UpcomingEvents, RecentActivities) — datos vía `GetDashboardData` (DataSet).
- Usuarios + Login: `UserObj` (Nombre, Correo, TipoAutenticacion, PasswordHash, ProveedorUserId) — OAuth Bearer (`api/Autentication`).
- Calendario: vista FullCalendar que consume eventos de Agenda.
- Correos: envío con plantillas HTML.

## Conventions (observed)
- Naming: C# PascalCase para tipos/miembros; carpetas por módulo y capa.
- Data access: cada Proxy hereda `DbWrapper` y llama SPs por nombre (`CommandType.StoredProcedure`); mapeo manual DataTable/DataSet → objetos en `*Mapp.cs`.
- Resultados: `OperationResult` (`Successful`, mensajes) en `Common.Domain`; cada App devuelve `out OperationResult`.
- DI: Unity registra Apps y Proxies en `UnityConfig.cs`.
- API: rutas `api/{Controller}/{Action}` con `RoutePrefix`, `[HttpPost]`, documentación `Swashbuckle` (Swagger).
- MVC: `BaseController` con `HttpClientConnection`; filtros propios `[Autenticated]` / `[NoAutenticated]`.
- Serialización API: `DefaultContractResolver` mantiene PascalCase (no camelCase).

## Key Findings / Technical Debt
- `DespachoJuridicoDESIWebApi/Controllers/RecorController.cs` define la clase activa `RecordController` (en el .csproj); `Controllers/RecordController.cs` existe en disco pero está EXCLUIDO del .csproj → código muerto con el mismo nombre de clase/ruta.
- Inconsistencia de modelo: `RecordEntity` (en `Record.Messages/RecordMessages.cs`) vs `RecordObj` (en `Record.Domain`).
- `[Authorize]` comentado en `CaseController`, `AgendaController`, `DashboardController`, `EventTypeController` (WebApi) → endpoints sin protección.
- `UserApp.AutenticacionParaToken` compara hash sin salt.
- `RecordProxy.DeleteRecord` ignora el SP y devuelve `DateTime.Now` (bug).
- `AgendaApp.PopulateRelatedObjects` provoca N+1 consultas.
- Sin paginación, sin validación centralizada y sin pruebas automatizadas.
- `AgendaObj` incluye campos Google (`GoogleEventId`, `SincronizadoGoogle`, `FechaSincronizacion`) pero no hay sincronización real con Google/Outlook.

## Functionality Gaps for a Law Firm (despacho jurídico)
- Actuaciones procesales / historial del expediente.
- Partes del proceso y contraparte.
- Materia jurídica e instancias (primera, segunda, amparo, etc.).
- Plazos / términos con cálculo de días hábiles y alertas.
- Recordatorios reales (flags `GeneraEvento` / `GeneraNotificacion` sin uso).
- Tareas internas y asignación de abogado responsable (`CaseObj` no lo tiene).
- Sincronización real con Google Calendar / Outlook.
- Gestión documental: adjuntos y plantillas de escritos.
- Finanzas: honorarios, pagos, gastos, facturación (CFDI) y estado de cuenta.
- Roles / permisos y auditoría / bitácora.
- Recuperación de contraseña y MFA.
- Notificaciones in-app reales (el panel actual es estático).
- Reportes y exportación.
- Catálogos adicionales y portal del cliente.

## Notes
- Misión actual es SOLO de análisis: no se implementa ni modifica código de la aplicación.
- Toda verificación de hallazgos debe hacerse contra el código real (Reviewer).

## Current Status
- MISIÓN COMPLETA Y VERIFICADA. `.opencode/todo.md` = 9/9 líneas de progreso completas (5 checkboxes `[x]`: S1.1.1, S1.1.2, S1.2.1, S1.2.2, S2.1.1 + 4 líneas `status: completed`). Sin `[ ]` pendientes.
- `.opencode/integration-status.md`: PASS (20 gaps F1–F20 + 17 hallazgos D1–D17).
- `.opencode/sync-issues.md`: contiene SOLO el encabezado `# Sync Issues` → el verificador del orquestador (`getSyncIssueLines` en `opencode-orchestrator/dist/index.js`) cuenta 0 issues. REGLA DEL HARNESS: cuenta toda línea no vacía excepto `---` y el encabezado exacto `# Sync Issues`; por eso NO añadir líneas de texto libre a ese archivo.
- Reviewer verificó todos los hallazgos contra el código real. No se modificó código de la aplicación.
- Entregables generados:
  - `.opencode/docs/inventario-funcionalidad.md` (S1.1.1/S1.1.2): módulos/entidades, App/Proxy+SPs, **37 endpoints API activos** (+ `/token` OAuth), **30 acciones MVC**, 11 vistas, correos.
  - `.opencode/docs/funcionalidad-faltante.md` (S1.2.1/S1.2.2): 20 brechas F1–F20 + 17 hallazgos de deuda técnica D1–D17 (con `archivo:línea`).
  - `.opencode/docs/funcionalidad-faltante-y-deuda-tecnica.md` (S1.2.1/S1.2.2): análisis complementario con priorización.
  - `.opencode/work-log.md`: sesiones ses_1/ses_2, estado de archivos.
- Hallazgos clave: `RecorController.cs` activo vs `RecordController.cs` excluido del .csproj; `[Authorize]` comentado en Case/Agenda/Dashboard/EventType; `CryptographyHelper` con claves hardcodeadas y reversible; `RecordProxy.DeleteRecord` devuelve `DateTime.Now` (bug); N+1 en `CaseApp`/`AgendaApp`; 0 tests; 0 `*.sql`; `Catalog` vacío; panel de notificaciones estático.
- Brechas críticas: actuaciones procesales, partes/contraparte, materia/instancia, plazos/términos, tareas+abogado responsable, documental, finanzas/CFDI, roles/permisos, auditoría, notificaciones reales, reportes, portal del cliente.
- Revisión unitaria adicional `ses_3 (Reviewer)` sobre S1.1.1/S1.1.2 (`inventario-funcionalidad.md`): PASS. Verificados 1:1 contra el código: 10 entidades de dominio + DTOs, interfaces App/Proxy y nombres de SP, 37 endpoints REST + `/token`, 30 acciones MVC, 11 vistas, `[Authorize]`, código muerto y correos.
- Hallazgo de contexto (no error del inventario, corregir en informe final): `context.md` afirma "Unity registra Apps y Proxies en `UnityConfig.cs`", pero `App_Start/UnityConfig.cs` es un no-op vacío; las registraciones reales están en `App_Start/Startup.cs` (líneas 52–79). `Catalog` tiene 4 proyectos vacíos (`Application`, `Domain`, `Messages`, `Proxy`), no 2.
- Limitación de verificación: sin proyecto de pruebas y sin `msbuild` (solo `dotnet 9`, incompatible con .NET Framework 4.8) → verificación estática por inspección de código (misión de solo análisis).

## Pending Tasks
- Ninguna. Misión cerrada; no hay TODOs pendientes ni incidencias abiertas en `.opencode/sync-issues.md`.
- Nota (no bloqueante): existe duplicación de alcance entre `funcionalidad-faltante.md` y `funcionalidad-faltante-y-deuda-tecnica.md`. Ambas cubren S1.2.1/S1.2.2; se conservan por trazabilidad. El canónico es `funcionalidad-faltante.md`.
- Recordatorio de verificación del harness: `.opencode/sync-issues.md` debe permanecer con solo el encabezado; `todo.md` debe mantener todos los checkboxes/status completos.
