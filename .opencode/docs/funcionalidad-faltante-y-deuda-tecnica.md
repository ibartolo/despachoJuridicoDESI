# Funcionalidad Faltante y Deuda Técnica

> Misión: análisis (solo lectura). Cubre S1.2.1 (funcionalidad faltante para un despacho) y S1.2.2 (deuda técnica).
> Toda afirmación se respalda con rutas de archivo del código real.

## 1. Resumen ejecutivo

El sistema actual es un **back-office básico** que cubre el registro de Clientes, Casos, Estatus de Caso, Expedientes, Juzgados, Agenda/Calendario y Tipos de Evento, con un Dashboard de resumen y autenticación OAuth. Lo que existe funciona como "CRUD + catálogos", pero **le falta el núcleo operativo de un despacho jurídico**: gestión procesal (actuaciones, plazos, partes, promociones), gestión documental, finanzas, seguridad por roles, auditoría y notificaciones reales.

Búsqueda de términos de dominio jurídico en el código (`*.cs`, `*.cshtml`): `Actuacion`=0, `Plazo`=0, `Termino`=0, `Vencimiento`=0, `Promocion`=0, `Contraparte`=0, `Materia`=0, `Amparo`=0, `Adjunto`=0, `Honorario`=0, `Gasto`=0, `Factura`=0, `CFDI`=0, `Permiso`=0, `Bitacora`=0, `Portal`=0. Esto confirma que **esas áreas no existen en el código**.

## 2. Funcionalidad faltante para un despacho jurídico (S1.2.1)

### 2.1 Gestión procesal (núcleo del negocio) — AUSENTE

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Actuaciones procesales / historial del expediente** | Es el registro cronológico de todo lo que ocurre en un juicio (acuerdos, resoluciones, notificaciones). Sin esto, el expediente es solo una ficha. | No existe entidad ni tabla. `RecordObj` solo tiene `RecordNumber` + `Comentarios` (`Record.Domain/RecordObj.cs`). |
| **Partes del proceso y contraparte** | Se necesita registrar actor/demandado/terceros y sus abogados. | `CaseObj` solo guarda `Cliente`, `EstatusCaso`, `NumeroCaso`, `Descripcion`, `MontoInicial` (`Case.Domain/CaseObj .cs`). No hay `Contraparte` (0 ocurrencias). |
| **Materia jurídica, vía e instancia** | Civil, penal, laboral, familiar, mercantil, amparo; primera/segunda instancia, incidentes. | No existe catálogo ni campo. `Materia`=0, `Instancia`=1 (solo un comentario), `Amparo`=0. |
| **Plazos y términos con cálculo de días hábiles** | El control de vencimientos es la función más crítica para no perder derechos. Requiere calendario de días inhábiles. | No existe. `Plazo`=0, `Termino`=0, `Vencimiento`=0. |
| **Promociones / escritos presentados** | Seguimiento de escritos y acuses. | No existe. `Promocion`=0. |
| **Etapas del proceso / línea de tiempo** | Ver avance del caso por etapas. | `StatusCaseObj` es solo un catálogo plano (`Nombre`, `Descripcion`, `Orden`) sin historial (`Case.Domain/StatusCaseObj.cs`). |

### 2.2 Agenda, recordatorios y notificaciones — PARCIAL / SIMULADO

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Recordatorios reales ligados a plazos/audiencias** | Evitar vencimientos y audiencias perdidas. | `StatusCaseObj` tiene flags `GeneraEvento` y `GeneraNotificacion` (`Case.Domain/StatusCaseObj.cs`) pero **no se usan en ningún flujo**; `CaseApp` no los lee ni dispara nada (`Case.Application/CaseApp.cs`). |
| **Sincronización real con Google Calendar / Outlook** | Agenda unificada del despacho. | `AgendaObj`/`AgendaEntity` tienen `GoogleEventId`, `SincronizadoGoogle`, `FechaSincronizacion` (`Agenda.Domain/AgendaObj.cs`, `AgendaEntity.cs`) pero **no hay ninguna llamada a API externa**; el calendario es FullCalendar propio (`Views/Calendar/Index.cshtml`). |
| **Notificaciones in-app reales** | Alertas de vencimientos, pagos, mensajes. | El panel de notificaciones está **hardcodeado/estático** con 5 ítems y badge fijo "3" (`Views/Shared/_Layout.cshtml`). No hay entidad, endpoint ni servicio de notificaciones. |
| **Recordatorios por correo programados** | Avisos automáticos. | Solo hay correos transaccionales al crear cliente/caso/expediente (`Controllers/CustomerController.cs`, `CaseController.cs`, `RecordController.cs`); no hay programación ni cola. |
| **Vista/gestión de Tipos de Evento** | Administrar el catálogo de eventos. | Hay API (`api/EventType`) pero **no hay controlador ni vista MVC**; no se administra desde la UI. |

### 2.3 Gestión de casos, tareas y responsables — AUSENTE

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Asignación de abogado responsable** | Saber quién lleva cada asunto. | `CaseObj` **no tiene campo de abogado/responsable** (`Case.Domain/CaseObj .cs`); `Abogado`=1, `Responsable`=0. Solo el correo de alta usa `SessionHelper.GetSessionUser()?.UserName` como texto "AbogadoAsignado" (`Controllers/CaseController.cs`). |
| **Tareas internas / checklist por caso** | Organización del trabajo del despacho. | No existe entidad ni vista. `Tarea`=5 (coincidencias de texto, no módulo). |
| **Equipo / usuarios por caso** | Colaboración. | No existe relación usuario–caso. |
| **Gestión de usuarios desde la UI** | Altas/bajas de personal. | Hay API (`api/Autentication/SaveOrUpdateUsuario`, `DeleteUsuario`) pero **no hay vista/controlador MVC de usuarios**. |

### 2.4 Gestión documental — AUSENTE

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Adjuntos / repositorio documental** | Expediente electrónico: contratos, promociones, acuses, identificaciones. | No existe almacenamiento de archivos. `Adjunto`=0, `Documento`=2 (solo referencias a "documentos" en textos). `EmailHelper` acepta `attachment` pero **nunca se usa** (`Helpers/EmailHelper.cs`). |
| **Plantillas de escritos** | Generación de promociones. | Solo existen 3 plantillas de **correo** HTML (`Templates/Template_AltaUsuario.html`, `Template_RegistroCaso.html`, `Template_RegistroExpediente.html`); no hay plantillas de escritos legales. |
| **Versionado de documentos** | Control de cambios. | No existe. |
| **Firma electrónica / acuses** | Formalidad. | No existe. |

### 2.5 Finanzas y facturación — AUSENTE

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Honorarios y presupuestos** | Cobro de servicios. | `CaseObj.MontoInicial` es el único dato económico (`Case.Domain/CaseObj .cs`); `Honorario`=0. |
| **Pagos y estado de cuenta** | Control de cobranza. | No existe. `Pago`=2 (coincidencias en comentarios/textos); el menú "Pagos" está **comentado** en `_Layout.cshtml`. |
| **Gastos del caso** | Costos (copias, peritos, traslados). | No existe. `Gasto`=0. |
| **Facturación (CFDI)** | Cumplimiento fiscal (México). | No existe. `Factura`=0, `CFDI`=0. |
| **Dashboard financiero real** | Cobranza y rentabilidad. | `SummaryCards.IngresosTotales` existe (`Dashboard.Domain/DashboardObj.cs`) pero depende de `GetDashboardData` (SP externo); no hay módulo de finanzas que lo alimente. |

### 2.6 Seguridad, roles, auditoría y cumplimiento — PARCIAL

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Roles y permisos (RBAC)** | Abogados vs administrativos vs socios; confidencialidad. | No existe. `Permiso`=0, `Rol`=68 (mayoría de `.csproj`/framework). El token OAuth asigna siempre `role=user` (`App_Start/Startup.cs`). |
| **Auditoría / bitácora de cambios** | Trazabilidad legal y de seguridad. | No hay tabla de auditoría. `Bitacora`=0; `Auditoria`=14 (probablemente `System.Diagnostics`/config). Solo campos `CreatedBy/UpdatedBy` en entidades. |
| **Recuperación de contraseña** | Operación básica. | No existe. |
| **MFA / segundo factor** | Seguridad de cuentas. | No existe. |
| **Bloqueo de endpoints API** | Proteger datos sensibles. | `[Authorize]` **comentado** en `CaseController`, `AgendaController`, `DashboardController`, `EventTypeController` (`DespachoJuridicoDESIWebApi/Controllers/*.cs`). |
| **Protección de contraseñas con hash + salt** | No almacenar credenciales de forma reversible. | `Cryptography.Encrypt` usa llaves **hardcodeadas** (`PasswordHash="P@@Sw0rd"`, `SaltKey`, `VIKey`) y es **reversible** (`Helpers/CryptographyHelper.cs`). No es hash. |
| **Expiración/refresh de token** | Gestión de sesión. | Token expira en 6 h sin refresh (`App_Start/Startup.cs`); la cookie de FormsAuth se crea sin `HttpOnly`/`Secure` explícitos (`Helpers/SessionHelper.cs`). |
| **CORS restrictivo** | Seguridad. | `app.UseCors(CorsOptions.AllowAll)` (`App_Start/Startup.cs`). |

### 2.7 Reportes, catálogos y portal — AUSENTE

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Reportes y exportación (PDF/Excel)** | Gestión y rendición de cuentas. | No existe. El menú "Reportes" está **comentado** en `_Layout.cshtml`; `Reporte`≈1. |
| **Catálogos adicionales** | Materias, tipos de asunto, tipos de documento, juzgados ya existe. | El proyecto `Catalog` está **vacío** (`Catalog.Application/ICatalogApp.cs`, `Catalog.Proxy/ICatalogProxi.cs`, `Catalog.Proxy/CatalogProxy.cs`). |
| **Portal del cliente** | Consulta de avances por el cliente. | No existe. `Portal`=0. |
| **Búsqueda global / filtros avanzados** | Localizar casos/expedientes. | DataTables en listados, pero sin búsqueda de servidor ni paginación (ver deuda). |
| **Configuración del despacho** | Datos fiscales, membretes. | El menú "Configuración" está **comentado** en `_Layout.cshtml`. |

### 2.8 Correo — solo salida

| Funcionalidad faltante | Por qué es crítica | Estado actual / evidencia |
|------------------------|--------------------|---------------------------|
| **Recepción / historial de correo** | Evidencia de comunicaciones. | Solo envío SMTP (`Helpers/EmailHelper.cs`). |
| **Notificaciones al cliente de cambios de estatus** | Transparencia. | No existe; solo altas de cliente/caso/expediente. |

## 3. Deuda técnica (S1.2.2)

| # | Deuda | Descripción | Evidencia |
|---|-------|-------------|-----------|
| 1 | **Controlador duplicado / código muerto** | `Controllers/RecorController.cs` contiene la clase activa `RecordController`; `Controllers/RecordController.cs` existe en disco pero está **excluido del .csproj**, con la misma clase/ruta. Confunde y puede romper el build si se incluye. | `DespachoJuridicoDESIWebApi/Controllers/RecorController.cs`; `DespachoJuridicoDESIWebApi.csproj` (incluye `RecorController.cs`, no `RecordController.cs`) |
| 2 | **Inconsistencia de modelo** | `RecordEntity` (request en `Record.Messages`) vs `RecordObj` (dominio en `Record.Domain`); `IRecordApp.SaveOrUpdateRecord` recibe `RecordEntity` mientras el resto usa `RecordObj`. | `Record.Messages/RecordMessages.cs`, `Record.Domain/RecordObj.cs`, `Record.Application/IRecordApp.cs` |
| 3 | **Endpoints API sin protección** | `[Authorize]` comentado en 4 controladores. | `Controllers/CaseController.cs`, `AgendaController.cs`, `DashboardController.cs`, `EventTypeController.cs` (WebApi) |
| 4 | **Credenciales reversibles / llaves hardcodeadas** | `Cryptography.Encrypt/Decrypt` usa clave, salt e IV fijos en código; es cifrado reversible, no hash. `UserApp.AutenticacionParaToken` compara `PasswordHash.Equals(pass)`. | `DespachoJuridicoDESIMVC/Helpers/CryptographyHelper.cs`; `User.Application/UserApp.cs`; `Controllers/HomeController.cs` |
| 5 | **Bug en `DeleteRecord`** | `RecordProxy.DeleteRecord` ejecuta el SP pero **ignora el resultado** y retorna `DateTime.Now` en lugar de la fecha del SP (`DeletedDt`). | `Record.Proxy/RecordProxy.cs` |
| 6 | **Consultas N+1** | `CaseApp.GetAllCases` hace 1 consulta por caso para estatus y 1 por caso para cliente; `AgendaApp.PopulateRelatedObjects` consulta caso y tipo de evento por cada evento. `GetCasesByClientId` repite patrón. | `Case.Application/CaseApp.cs`; `Agenda.Application/AgendaApp.cs` |
| 7 | **Sin pruebas automatizadas** | No hay proyecto de test ni paquetes xUnit/NUnit/MSTest. | Ningún `*.csproj` de test; `packages.config` sin frameworks de test |
| 8 | **Sin paginación** | Los listados devuelven todos los registros; DataTables pagina en cliente. | `Controllers/Home/...` no aplica; `Views/*/Index.cshtml` (DataTables cliente); proxies sin `@Page` |
| 9 | **Sin validación centralizada** | Validaciones dispersas en controladores/vistas; sin filtros de modelo ni `FluentValidation`. | `Controllers/*.cs` (validaciones manuales), `BaseController.IsValidEmail` |
| 10 | **Manejo de excepciones silencioso** | `catch` vacíos o que tragan errores; se pierde la causa. | `User.Application/UserApp.cs` (`catch (Exception ex) { }`); varios `catch (Exception ex)` sin log |
| 11 | **SPs fuera del control de versiones** | No hay scripts SQL/migraciones en el repositorio; la lógica de datos vive solo en SQL Server. | No existen `*.sql` en el repo |
| 12 | **Notificaciones hardcodeadas** | Panel de notificaciones estático en el layout. | `Views/Shared/_Layout.cshtml` |
| 13 | **Proyecto `Catalog` vacío** | Interfaces/clases sin miembros. | `Catalog.Application/ICatalogApp.cs`, `Catalog.Proxy/ICatalogProxi.cs`, `Catalog.Proxy/CatalogProxy.cs` |
| 14 | **Zona horaria fija** | `TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")` puede fallar en Linux/otros entornos. | `Helpers/SessionHelper.cs` |
| 15 | **CORS permisivo** | `AllowAll`. | `App_Start/Startup.cs` |
| 16 | **Secretos en configuración** | Credenciales SMTP en `Web.config` (`userEmail`, `passEmail`). | `Helpers/EmailHelper.cs`, `DespachoJuridicoDESIMVC/Web.config` |
| 17 | **Nombres inconsistentes** | Carpeta `Agenda.Messagess` (mal escrita) y `CourtMessages` (fuera del patrón `Court.Messages`); `ClientMassagesResponse`/`UserMassages` (typo "Massages"). | `Agenda.Messagess/`, `CourtMessages/`, `Customer.Messages/CustomerMessages.cs`, `User.Messages/UserMassagesResponse.cs` |
| 18 | **Sin transacciones explícitas** | Operaciones compuestas (caso + estatus, expediente + caso/juzgado) no usan transacción; `DbWrapper` ejecuta SPs individuales. | `SqlProxy/DbWrapper.cs`, `Case.Application/CaseApp.cs` |
| 19 | **`MapAuditFields` por reflexión** | Auditoría de `CreatedBy/UpdatedBy` por reflexión en el cliente MVC, no en el servidor. | `DAL/HttpClientConnection.cs` |
| 20 | **Swagger parcial** | `SwaggerResponse` solo en algunos endpoints (Dashboard, EventType, Record); falta en Case, Customer, Court, Agenda. | `Controllers/*.cs` |

## 4. Priorización sugerida (alto impacto)

1. **Gestión procesal**: actuaciones + plazos/términos con días hábiles y alertas (núcleo del valor).
2. **Partes del proceso, materia/instancia** y **asignación de abogado responsable**.
3. **Seguridad**: habilitar `[Authorize]`, RBAC, hash con salt, auditoría/bitácora.
4. **Gestión documental** (adjuntos) y **finanzas** (honorarios/pagos/facturación).
5. **Notificaciones reales** y **recordatorios** (usar `GeneraEvento`/`GeneraNotificacion`).
6. **Calidad**: tests, paginación, validación centralizada, corregir `DeleteRecord`, limpiar controlador duplicado.

## 5. Evidencia de rutas consultadas

- Entidades: `*/Domain/*.cs`, `Common.Domain/*.cs`, `Record.Messages/RecordMessages.cs`
- Apps/Proxies: `*/Application/*.cs`, `*/Proxy/*.cs`, `SqlProxy/*.cs`
- API: `DespachoJuridicoDESIWebApi/Controllers/*.cs`, `App_Start/Startup.cs`, `*.csproj`
- MVC: `DespachoJuridicoDESIMVC/Controllers/*.cs`, `DAL/*.cs`, `Helpers/*.cs`, `Views/**`, `Templates/*`, `*.csproj`
- Búsqueda de términos de dominio y de frameworks de test (PowerShell `Select-String`).
