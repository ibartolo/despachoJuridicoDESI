# Inventario de Funcionalidad Existente

> Misión: análisis (solo lectura). Documento generado a partir del código real.
> Alcance: S1.1.1 (módulos y entidades) y S1.1.2 (endpoints Web API y acciones MVC).

## 1. Resumen ejecutivo

- Solución .NET Framework compuesta por 2 aplicaciones web y bibliotecas de dominio por módulo.
  - `DespachoJuridicoDESIMVC` — ASP.NET MVC 5 (back-office, vistas Razor + AJAX vía `HttpClientConnection`).
  - `DespachoJuridicoDESIWebApi` — ASP.NET Web API 2 (OAuth Bearer, Swagger, Unity DI).
- Patrón por módulo: `X.Domain` (objetos), `X.Application` (App + interfaz), `X.Proxy` (wrapper de stored procedures + `*Mapp`), `X.Messages` (DTOs request/response).
- Acceso a datos 100% por stored procedures (`SqlProxy.BaseDbWrapper`/`DbWrapper`), sin ORM.
- Módulos funcionales reales: Clientes, Casos, Estatus de Caso, Expedientes, Juzgados, Agenda/Calendario, Tipos de Evento, Dashboard, Usuarios/Autenticación, Correos.
- `Catalog` existe como proyecto pero está **vacío** (clase `ICatalogApp` interna vacía, `ICatalogProxi` sin miembros, `CatalogProxy` sin métodos).
- No existe proyecto de pruebas.

## 2. Módulos y entidades (S1.1.1)

| Módulo | Entidad/Objeto (dominio) | Campos | App (interfaz) | Proxy (interfaz) | Evidencia |
|--------|--------------------------|--------|----------------|------------------|-----------|
| Clientes | `ClientObj` (`Customer.Domain`) | Id, Nombre, Telefono, Correo, Estatus, CreatedBy/Dt, UpdatedBy/Dt | `IClientApp` | `IClientProxy` | `Customer.Domain/ClientObj.cs`, `Customer.Application/IClientApp.cs`, `Customer.Proxy/IClientProxy.cs` |
| Clientes (entidad request) | `ClientEntity` (`Common.Domain`) | Id, Nombre, Telefono, Correo, Estatus, auditoría | — | — | `Common.Domain/ClientEntity.cs` |
| Casos | `CaseObj` (`Case.Domain`) | Id, Cliente, EstatusCaso, NumeroCaso, Descripcion, MontoInicial, Estatus, auditoría | `ICaseApp` | `ICaseProxy` | `Case.Domain/CaseObj .cs`, `Case.Application/ICaseApp.cs`, `Case.Proxy/ICaseProxy.cs` |
| Casos (entidad request) | `CaseEntity` (`Common.Domain`) | Id, Cliente, EstatusCaso, NumeroCaso, Descripcion, MontoInicial, Estatus, auditoría | — | — | `Common.Domain/CaseEntity.cs` |
| Estatus de Caso | `StatusCaseObj` (`Case.Domain`) | Id, Nombre, Descripcion, Orden, GeneraEvento, GeneraNotificacion, Estatus, auditoría | `ICaseApp` (mismos métodos) | `IStatusCaseProxy` | `Case.Domain/StatusCaseObj.cs`, `Case.Proxy/IStatusCaseProxy.cs` |
| Estatus de Caso (entidad request) | `StatusCaseEntity` (`Common.Domain`) | Id, Nombre, Descripcion, Orden, GeneraEvento, GeneraNotificacion, Estatus, auditoría | — | — | `Common.Domain/StatusCaseEntity.cs` |
| Expedientes | `RecordObj` (`Record.Domain`) | Id, Case, Court, RecordNumber, Comentarios, Status, auditoría | `IRecordApp` | `IRecordProxy` | `Record.Domain/RecordObj.cs`, `Record.Application/IRecordApp.cs`, `Record.Proxy/IRecordProxy.cs` |
| Expedientes (entidad request) | `RecordEntity` (`Record.Messages`) | Id, Case, Court, RecordNumber, Comentarios, Status, auditoría | — | — | `Record.Messages/RecordMessages.cs` |
| Juzgados | `CourtObj` (`Court.Domain`) | Id, Nombre, Direccion, Colonia, Ciudad, Estado, CodigoPostal, Telefono, Horario, Observaciones, Estatus, auditoría | `ICourtApp` | `ICourtProxy` | `Court.Domain/Court.cs`, `Court.Application/ICourtApp.cs`, `Court.Proxy/ICourtProxy.cs` |
| Juzgados (entidad request) | `CourtEntity` (`Common.Domain`) | Id, Nombre, Direccion, Colonia, Ciudad, Estado, CodigoPostal, Telefono, Horario, Observaciones, Estatus, auditoría | — | — | `Common.Domain/CourtEntity.cs` |
| Agenda | `AgendaObj` (`Agenda.Domain`) | Id, Caso, TipoEvento, Titulo, Descripcion, FechaInicio, FechaFin, GoogleEventId, SincronizadoGoogle, FechaSincronizacion, Estatus, auditoría | `IAgendaApp` | `IAgendaProxy` | `Agenda.Domain/AgendaObj.cs`, `Agenda.Application/IAgendaApp.cs`, `Agenda.Proxy/IAgendaProxy.cs` |
| Agenda (entidad de transporte) | `AgendaEntity` (`Agenda.Domain`) | Id, CasoId, TipoEventoId, Titulo, Descripcion, FechaInicio/Fin, GoogleEventId, SincronizadoGoogle, FechaSincronizacion, Estatus, auditoría, NombreCaso, NombreTipoEvento | — | — | `Agenda.Domain/AgendaEntity.cs` |
| Tipos de Evento | `EventTypeObj` (`EventType.Domain`) | Id, Nombre, Descripcion, Estatus, auditoría | `IEventTypeApp` | `IEventTypeProxy` | `EventType.Domain/EventTypeObj.cs`, `EventType.Application/IEventTypeApp.cs`, `EventType.Proxy/IEventTypeProxy.cs` |
| Tipos de Evento (entidad request) | `EventTypeEntity` (`EventType.Messages`) | Id, Nombre, Descripcion, Estatus, auditoría | — | — | `EventType.Messages/EventTypeMessages.cs` |
| Dashboard | `DashboardObj` (`Dashboard.Domain`) | Summary (`SummaryCards`), UpcomingEvents (`UpcomingEvent`), RecentActivities (`RecentActivity`) | `IDashboardApp` | `IDashboardProxy` | `Dashboard.Domain/DashboardObj.cs`, `Dashboard.Application/IDashboardApp.cs`, `Dashboard.Proxy/IDashboardProxy.cs` |
| Usuarios / Login | `UserObj` (`User.Domain`) | Id, Nombre, Correo, TipoAutenticacion, PasswordHash, ProveedorUserId, Estatus, auditoría | `IUserApp` | `IUserProxy` | `User.Domain/UserObj.cs`, `User.Application/IUserApp.cs`, `User.Proxy/IUserProxy.cs` |
| Comunes | `OperationResult`, `SystemMessage` | `Successful`, `SystemMessages` | — | — | `Common.Domain/OperationResult.cs`, `Common.Domain/SystemMessage.cs` |
| Catálogo (vacío) | — | — | `ICatalogApp` (internal, vacía) | `ICatalogProxi` (vacía) | `Catalog.Application/ICatalogApp.cs`, `Catalog.Proxy/ICatalogProxi.cs`, `Catalog.Proxy/CatalogProxy.cs` |

Notas de entidades:
- `RecordObj` (dominio) expone propiedades de solo lectura; los request usan `RecordEntity` (`Record.Messages`). Inconsistencia `RecordEntity` vs `RecordObj` (ver deuda técnica S1.2.2).
- `AgendaObj` incluye campos de Google (`GoogleEventId`, `SincronizadoGoogle`, `FechaSincronizacion`) pero no hay integración real (solo se persisten/mapean).

## 3. Operaciones de App y stored procedures por Proxy

| Módulo | Métodos de App (`I*App`) | Stored procedures invocados (`I*Proxy` → `DbWrapper`) |
|--------|--------------------------|-------------------------------------------------------|
| Clientes | GetAllClientes, GetClienteById, DeleteCliente, SaveOrUpdateCliente | `GetAllCliente`, `GetClienteById`, `DeleteCliente`, `SaveOrUpdateCliente` |
| Casos | GetAllCases, GetCaseById, GetCasesByClientId, DeleteCase, SaveOrUpdateCase | `GetAllCaso`, `GetCasoById`, `GetCasoByClienteId`, `DeleteCaso`, `SaveOrUpdateCaso` |
| Estatus de Caso | GetAllStatusCase, GetStatusCaseById, DeleteStatusCase, SaveOrUpdateStatusCase | `GetAllEstatusCaso`, `GetEstatusCasoById`, `DeleteEstatusCaso`, `SaveOrUpdateEstatusCaso` |
| Expedientes | GetAllRecords, GetRecordsByCaseId, GetRecordById, DeleteRecord, SaveOrUpdateRecord | `GetAllExpediente`, `GetExpedienteByCasoId`, `GetExpedienteById`, `DeleteExpediente`, `SaveOrUpdateExpediente` |
| Juzgados | GetAllCourts, GetCourtById, DeleteCourt, SaveOrUpdateCourt | `GetAllJuzgado`, `GetJuzgadoById`, `DeleteJuzgado`, `SaveOrUpdateJuzgado` |
| Agenda | GetAllAgenda, GetAgendaById, GetAgendaByCasoId, GetAgendaByFecha, DeleteAgenda, SaveOrUpdateAgenda | `GetAllAgenda`, `GetAgendaById`, `GetAgendaByCasoId`, `GetAgendaByFecha`, `DeleteAgenda`, `SaveOrUpdateAgenda` |
| Tipos de Evento | GetAllEventTypes, GetEventTypeById, DeleteEventType, SaveOrUpdateEventType | `GetAllTipoEvento`, `GetTipoEventoById`, `DeleteTipoEvento`, `SaveOrUpdateTipoEvento` |
| Usuarios | GetUsuarioByCorreo, GetUsuarioByProveedor, DeleteUsuario, SaveOrUpdateUsuario, AutenticacionParaToken | `GetUsuarioByCorreo`, `GetUsuarioByProveedor`, `DeleteUsuario`, `SaveOrUpdateUsuario` |
| Dashboard | GetDashboardData | `GetDashboardData` (DataSet de 3 tablas) |

Evidencia: `*Proxy/*Proxy.cs` (cada llamada usa `GetObject("<SP>", CommandType.StoredProcedure, ...)`).

## 4. Endpoints Web API (S1.1.2)

Convención: `[RoutePrefix("api/<Controller>")]` + `[HttpPost]`/`[HttpDelete]` + `[Route("<Action>")]`. Serialización PascalCase (`WebApiConfig.cs`).

### 4.1 Endpoints activos (compilados en el .csproj)

| # | Verbo | Ruta completa | Método | Evidencia |
|---|-------|---------------|--------|-----------|
| 1 | POST | `/api/Agenda/GetAll` | `AgendaController.GetAll` | `DespachoJuridicoDESIWebApi/Controllers/AgendaController.cs` |
| 2 | POST | `/api/Agenda/GetById` | `AgendaController.GetById` | idem |
| 3 | POST | `/api/Agenda/GetByCasoId` | `AgendaController.GetByCasoId` | idem |
| 4 | POST | `/api/Agenda/GetByFecha` | `AgendaController.GetByFecha` | idem |
| 5 | POST | `/api/Agenda/Delete` | `AgendaController.Delete` | idem |
| 6 | POST | `/api/Agenda/SaveOrUpdate` | `AgendaController.SaveOrUpdate` | idem |
| 7 | POST | `/api/Autentication/GetUsuarioByCorreo` | `AutenticationController.GetUsuarioByCorreo` (`[AllowAnonymous]`) | `Controllers/AutenticationController.cs` |
| 8 | GET | `/api/Autentication/GetUsuarioByProveedor` | `AutenticationController.GetUsuarioByProveedor` (`[Authorize]`) | idem |
| 9 | DELETE | `/api/Autentication/DeleteUsuario` | `AutenticationController.DeleteUsuario` | idem |
| 10 | POST | `/api/Autentication/SaveOrUpdateUsuario` | `AutenticationController.SaveOrUpdateUsuario` | idem |
| 11 | POST | `/api/Case/StatusCase/List` | `CaseController.GetAll` | `Controllers/CaseController.cs` |
| 12 | POST | `/api/Case/StatusCase/First` | `CaseController.GetById` | idem |
| 13 | DELETE | `/api/Case/StatusCase` | `CaseController.Delete` | idem |
| 14 | POST | `/api/Case/StatusCase` | `CaseController.SaveOrUpdate` | idem |
| 15 | POST | `/api/Case/List` | `CaseController.GetAllCases` | idem |
| 16 | POST | `/api/Case/First` | `CaseController.GetCaseById` | idem |
| 17 | POST | `/api/Case/Cliente` | `CaseController.GetCasesByClientId` | idem |
| 18 | DELETE | `/api/Case` | `CaseController.DeleteCase` | idem |
| 19 | POST | `/api/Case` | `CaseController.SaveOrUpdateCase` | idem |
| 20 | POST | `/api/Court/GetAll` | `CourtController.GetAll` (`[Authorize]`) | `Controllers/CourtController.cs` |
| 21 | POST | `/api/Court/GetById` | `CourtController.GetById` | idem |
| 22 | POST | `/api/Court/Delete` | `CourtController.Delete` | idem |
| 23 | POST | `/api/Court/SaveOrUpdate` | `CourtController.SaveOrUpdate` | idem |
| 24 | POST | `/api/Customer/List` | `CustomerController.GetAllClientes` (`[Authorize]`) | `Controllers/CustomerController.cs` |
| 25 | POST | `/api/Customer/First` | `CustomerController.GetClienteById` | idem |
| 26 | DELETE | `/api/Customer` | `CustomerController.DeleteCliente` | idem |
| 27 | POST | `/api/Customer` | `CustomerController.SaveOrUpdateCliente` | idem |
| 28 | POST | `/api/Dashboard/GetDashboardData` | `DashboardController.GetDashboardData` | `Controllers/DashboardController.cs` |
| 29 | POST | `/api/EventType/GetAll` | `EventTypeController.GetAll` | `Controllers/EventTypeController.cs` |
| 30 | POST | `/api/EventType/GetById` | `EventTypeController.GetById` | idem |
| 31 | POST | `/api/EventType/Delete` | `EventTypeController.Delete` | idem |
| 32 | POST | `/api/EventType/SaveOrUpdate` | `EventTypeController.SaveOrUpdate` | idem |
| 33 | POST | `/api/Record/GetAll` | `RecordController.GetAll` (`[Authorize]`, definido en `RecorController.cs`) | `Controllers/RecorController.cs` |
| 34 | POST | `/api/Record/GetByCaseId` | `RecordController.GetByCaseId` | idem |
| 35 | POST | `/api/Record/Id` | `RecordController.GetById` | idem |
| 36 | POST | `/api/Record/Delete` | `RecordController.Delete` | idem |
| 37 | POST | `/api/Record` | `RecordController.SaveOrUpdate` | idem |
| 38 | POST | `/token` | OAuth token endpoint (`SimpleAuthorizationServerProvider`) | `App_Start/Startup.cs` |

**Total: 37 endpoints REST + 1 endpoint OAuth (`/token`).**

### 4.2 Código muerto (NO compilado)

| Verbo | Ruta | Método | Estado | Evidencia |
|-------|------|--------|--------|-----------|
| POST | `/api/Record/GetByCaseId` | `RecordController.GetByCaseId` | **Excluido del .csproj** (duplica ruta/clase) | `Controllers/RecordController.cs` vs `DespachoJuridicoDESIWebApi.csproj` (solo incluye `RecorController.cs`) |
| POST | `/api/Record/GetById` | `RecordController.GetById` | Excluido del .csproj | idem |
| POST | `/api/Record/Delete` | `RecordController.Delete` | Excluido del .csproj | idem |
| POST | `/api/Record/SaveOrUpdate` | `RecordController.SaveOrUpdate` | Excluido del .csproj | idem |

Evidencia csproj: `<Compile Include="Controllers\RecorController.cs" />` presente; `Controllers\RecordController.cs` **no** aparece.

### 4.3 Protección de endpoints

| Controlador API | `[Authorize]` | Observación |
|-----------------|---------------|-------------|
| `CourtController` | Activo | — |
| `CustomerController` | Activo | — |
| `RecordController` (en `RecorController.cs`) | Activo | — |
| `AutenticationController` | Parcial | `GetUsuarioByCorreo` `[AllowAnonymous]`, `GetUsuarioPorProveedor` `[Authorize]`, Delete/Save sin atributo |
| `CaseController` | **Comentado** (`//[Authorize]`) | Endpoints sin protección |
| `AgendaController` | **Comentado** | Endpoints sin protección |
| `DashboardController` | **Comentado** | Endpoints sin protección |
| `EventTypeController` | **Comentado** | Endpoints sin protección |

## 5. Acciones MVC (S1.1.2)

Convención: controladores heredan `BaseController` (expone `httpClient`), decorados con `[Autenticated]` salvo Home. Las vistas AJAX llaman al API a través de `HttpClientConnection.*` (`DAL/HttpClientConnection.<Módulo>.cs`).

| # | Controlador MVC | Acción | Tipo | Ruta API consumida | Vista | Evidencia |
|---|-----------------|--------|------|--------------------|-------|-----------|
| 1 | `HomeController` | `Autentication` | Vista (`[NoAutenticated]`) | — | `Views/Home/Autentication.cshtml` | `Controllers/HomeController.cs` |
| 2 | `HomeController` | `Index` | Vista (`[Autenticated]`) | `api/Dashboard/GetDashboardData` | `Views/Home/Index.cshtml` | idem |
| 3 | `HomeController` | `AutenticacionUsuario` | AJAX/JSON | `api/Autentication/GetUsuarioByCorreo` + `/token` | — | idem |
| 4 | `HomeController` | `LogOut` | Redirect | — | — | idem |
| 5 | `CaseController` | `Create` | Vista | `api/Case/First`, `api/Customer/List`, `api/Case/StatusCase/List` | `Views/Case/Create.cshtml` | `Controllers/CaseController.cs` |
| 6 | `CaseController` | `Index` | Vista | — | `Views/Case/Index.cshtml` | idem |
| 7 | `CaseController` | `GetAllCases` | AJAX/JSON | `api/Case/List` | — | idem |
| 8 | `CaseController` | `SaveOrUpdateCase` | POST | `api/Case` (+ envío correo alta caso) | — | idem |
| 9 | `CustomerController` | `Create` | Vista | `api/Customer/First` | `Views/Customer/Create.cshtml` | `Controllers/CustomerController.cs` |
| 10 | `CustomerController` | `Index` | Vista | — | `Views/Customer/Index.cshtml` | idem |
| 11 | `CustomerController` | `GetAllClientes` | AJAX/JSON | `api/Customer/List` | — | idem |
| 12 | `CustomerController` | `SaveOrUpdateCliente` | POST | `api/Customer` (+ correo alta cliente) | — | idem |
| 13 | `CourtController` | `Create` | Vista | `api/Court/GetById` | `Views/Court/Create.cshtml` | `Controllers/CourtController.cs` |
| 14 | `CourtController` | `Index` | Vista | — | `Views/Court/Index.cshtml` | idem |
| 15 | `CourtController` | `GetAllCourts` | AJAX/JSON | `api/Court/GetAll` | — | idem |
| 16 | `CourtController` | `GetCourtById` | AJAX/JSON | `api/Court/GetById` | — | idem |
| 17 | `CourtController` | `SaveOrUpdateCourt` | POST | `api/Court/SaveOrUpdate` | — | idem |
| 18 | `CourtController` | `DeleteCourt` | JSON | `api/Court/Delete` | — | idem |
| 19 | `RecordController` | `Create` | Vista | `api/Record/Id`, `api/Case/List`, `api/Court/GetAll` | `Views/Record/Create.cshtml` | `Controllers/RecordController.cs` |
| 20 | `RecordController` | `Index` | Vista | — | `Views/Record/Index.cshtml` | idem |
| 21 | `RecordController` | `GetAllRecords` | AJAX/JSON | `api/Record/GetAll` | — | idem |
| 22 | `RecordController` | `GetRecordById` | AJAX/JSON | `api/Record/Id` | — | idem |
| 23 | `RecordController` | `SaveOrUpdateRecord` | POST | `api/Record` (+ correo alta expediente) | — | idem |
| 24 | `RecordController` | `DeleteRecord` | JSON | `api/Record/Delete` | — | idem |
| 25 | `CalendarController` | `Index` | Vista | — | `Views/Calendar/Index.cshtml` | `Controllers/CalendarController.cs` |
| 26 | `CalendarController` | `GetAllEvents` | AJAX/JSON | `api/Agenda/GetAll` | — | idem |
| 27 | `CalendarController` | `GetCases` | AJAX/JSON | `api/Case/List` | — | idem |
| 28 | `CalendarController` | `GetEventTypes` | AJAX/JSON | `api/EventType/GetAll` | — | idem |
| 29 | `CalendarController` | `SaveOrUpdateAgenda` | JSON | `api/Agenda/SaveOrUpdate` | — | idem |
| 30 | `CalendarController` | `DeleteAgenda` | JSON | `api/Agenda/Delete` | — | idem |

**Total: 30 acciones MVC** (4 Home + 4 Case + 4 Customer + 6 Court + 6 Record + 6 Calendar).

Controladores MVC registrados en `DespachoJuridicoDESIMVC.csproj`: `BaseController`, `CalendarController`, `CaseController`, `CourtController`, `CustomerController`, `HomeController`, `RecordController`.

### 5.1 Vistas existentes

| Vista | Ruta | Propósito |
|-------|------|-----------|
| Dashboard | `Views/Home/Index.cshtml` | Tarjetas resumen, próximos eventos, actividad reciente |
| Login | `Views/Home/Autentication.cshtml` (+ `Views/Shared/_Autentication.cshtml`) | Autenticación |
| Casos (lista) | `Views/Case/Index.cshtml` | DataTable de casos |
| Caso (form) | `Views/Case/Create.cshtml` | Alta/edición de caso |
| Clientes (lista) | `Views/Customer/Index.cshtml` | DataTable de clientes |
| Cliente (form) | `Views/Customer/Create.cshtml` | Alta/edición de cliente |
| Juzgados (lista) | `Views/Court/Index.cshtml` | DataTable de juzgados |
| Juzgado (form) | `Views/Court/Create.cshtml` | Alta/edición de juzgado |
| Expedientes (lista) | `Views/Record/Index.cshtml` | DataTable de expedientes |
| Expediente (form) | `Views/Record/Create.cshtml` | Alta/edición de expediente |
| Calendario | `Views/Calendar/Index.cshtml` | FullCalendar + modal nueva cita |
| Layout | `Views/Shared/_Layout.cshtml` | Menú: Dashboard, Casos, Expedientes, Juzgados, Clientes, Calendario; panel de notificaciones estático |

Observaciones de UI:
- El menú lateral (`_Layout.cshtml`) tiene comentadas las entradas de **Pagos**, **Reportes** y **Configuración** (no implementadas).
- El panel de notificaciones (`_Layout.cshtml`) contiene datos **hardcodeados/estáticos**; el badge muestra "3" fijo.
- No hay vistas para Tipos de Evento ni para administración de Usuarios (solo endpoints API).

## 6. Correos (envío de notificaciones)

| Plantilla | Ruta | Disparador | Evidencia |
|-----------|------|------------|-----------|
| Alta de usuario/cliente | `DespachoJuridicoDESIMVC/Templates/Template_AltaUsuario.html` | `CustomerController.SaveOrUpdateCliente` (cliente nuevo) | `Controllers/CustomerController.cs` |
| Registro de caso | `DespachoJuridicoDESIMVC/Templates/Template_RegistroCaso.html` | `CaseController.SaveOrUpdateCase` (caso nuevo) | `Controllers/CaseController.cs` |
| Registro de expediente | `DespachoJuridicoDESIMVC/Templates/Template_RegistroExpediente.html` | `RecordController.SaveOrUpdateRecord` (expediente nuevo) | `Controllers/RecordController.cs` |

Envío vía SMTP con `EmailHelper.EnvioEmaiil` (config `userEmail`, `passEmail`, `smtpClient`, `port` en `Web.config`). Evidencia: `Helpers/EmailHelper.cs`.

## 7. Evidencia de rutas consultadas

- Dominio: `Agenda.Domain/`, `Case.Domain/`, `Common.Domain/`, `Court.Domain/`, `Customer.Domain/`, `Dashboard.Domain/`, `EventType.Domain/`, `Record.Domain/`, `User.Domain/`
- Aplicación: `*Application/` (App + interfaz)
- Datos: `*Proxy/` (Proxy + Mapp + interfaz) y `SqlProxy/` (`BaseDbWrapper.cs`, `DbWrapper.cs`)
- Mensajes: `*Messages/`, `Agenda.Messagess/`, `CourtMessages/`
- API: `DespachoJuridicoDESIWebApi/Controllers/`, `App_Start/WebApiConfig.cs`, `App_Start/Startup.cs`, `DespachoJuridicoDESIWebApi.csproj`
- MVC: `DespachoJuridicoDESIMVC/Controllers/`, `DAL/HttpClientConnection*.cs`, `Views/`, `Helpers/`, `Templates/`, `DespachoJuridicoDESIMVC.csproj`
