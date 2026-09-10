# Funcionalidad Faltante y Deuda Técnica

> Misión: análisis (solo lectura). Documento generado a partir del código real.
> Alcance: S1.2.1 (funcionalidad faltante para un despacho jurídico) y S1.2.2 (deuda técnica).
> Complementa a `.opencode/docs/inventario-funcionalidad.md` (S1.1.1 / S1.1.2).

## 1. Resumen ejecutivo

El sistema actual es un **back-office básico** que administra catálogos y registros (clientes, casos, estatus de caso, expedientes, juzgados, agenda, tipos de evento, dashboard, usuarios). Cubre el registro administrativo, pero **no cubre los procesos sustantivos de un despacho jurídico**: seguimiento procesal, plazos, asignación de responsables, documental, finanzas, seguridad por roles y reportes.

Además presenta deuda técnica relevante (código duplicado/muerto, endpoints sin autorización, hashing débil, un bug en `DeleteRecord`, consultas N+1 y ausencia total de pruebas).

---

## 2. Funcionalidad faltante (S1.2.1)

| # | Área | Descripción de lo faltante | Evidencia en código (lo que existe y demuestra la ausencia) |
|---|------|----------------------------|-------------------------------------------------------------|
| F1 | Actuaciones procesales / historial | No existe entidad ni vista para registrar actuaciones (acuerdos, notificaciones, promociones, etapas) con fecha, tipo y responsable. El expediente (`RecordObj`) solo guarda `RecordNumber` y `Comentarios`. | `Record.Domain/RecordObj.cs` (solo Id, Case, Court, RecordNumber, Comentarios, Status, auditoría). No hay `Actuacion*` en ningún proyecto. |
| F2 | Partes del proceso / contraparte | No se modelan partes (actor, demandado, terceros, contraparte, abogado contrario). `CaseObj` solo referencia al `Cliente`. | `Case.Domain/CaseObj .cs` (Id, Cliente, EstatusCaso, NumeroCaso, Descripcion, MontoInicial). |
| F3 | Materia jurídica e instancias | No hay catálogo de materia (civil, penal, laboral, familiar, mercantil, amparo) ni de instancia (primera, segunda, amparo, ejecución). | No existen proyectos/clases `Materia*` ni `Instancia*`. `CaseObj` no tiene campos de materia/instancia. |
| F4 | Plazos y términos | No existe entidad de plazos/términos ni cálculo de días hábiles, vencimientos o semáforos. | No hay `Plazo*`/`Termino*`. El módulo `Agenda` solo tiene `FechaInicio`/`FechaFin` sin reglas de cómputo. |
| F5 | Recordatorios / alertas reales | `StatusCaseObj` define `GeneraEvento` y `GeneraNotificacion`, pero no hay lógica que los consuma (no se generan eventos ni notificaciones). | `Case.Domain/StatusCaseObj.cs` (flags `GeneraEvento`, `GeneraNotificacion`); `Agenda.Application/AgendaApp.cs` no los lee. |
| F6 | Tareas internas / asignación de abogado responsable | No hay tareas internas ni asignación de abogado/equipo a un caso. `CaseObj` no tiene campo de responsable. | `Case.Domain/CaseObj .cs` (sin `AbogadoId`/`ResponsableId`); no hay proyecto `Task*`/`Tarea*`. |
| F7 | Sincronización real Google/Outlook | `AgendaObj`/`AgendaEntity` tienen `GoogleEventId`, `SincronizadoGoogle`, `FechaSincronizacion`, pero no existe cliente/servicio de sincronización; solo se persisten los campos. | `Agenda.Domain/AgendaObj.cs`, `Agenda.Domain/AgendaEntity.cs`; no hay referencias a Google Calendar/Graph API en el código. |
| F8 | Gestión documental (adjuntos) | No hay adjuntos/archivos ligados a casos o expedientes, ni almacenamiento de documentos. | No existe entidad `Documento*`/`Adjunto*` ni carpeta de subida. |
| F9 | Plantillas de escritos | Se envían correos con plantillas HTML, pero no hay generación de escritos/promociones a partir de plantillas. | Existe utilería de correo; no hay `Plantilla*` de escritos ni generación de documentos. |
| F10 | Finanzas: honorarios, pagos, gastos | `CaseObj.MontoInicial` es el único dato económico; no hay honorarios, pagos, gastos, saldos ni estado de cuenta. | `Case.Domain/CaseObj .cs` (solo `MontoInicial`); no hay proyecto `Pago*`/`Honorario*`/`Gasto*`. |
| F11 | Facturación (CFDI) | No existe emisión de facturas ni timbrado CFDI ni datos fiscales del cliente. | `Customer.Domain/ClientObj.cs` (sin RFC, régimen fiscal, dirección fiscal); no hay módulo de facturación. |
| F12 | Roles / permisos | No hay roles ni permisos por módulo/acción; la autenticación es binaria (autenticado o no). | Filtros `[Autenticated]`/`[NoAutenticated]` en MVC; no hay `Rol*`/`Permiso*` ni `[Authorize(Roles=...)]`. |
| F13 | Auditoría / bitácora | Existen campos `CreatedBy/CreatedDt/UpdatedBy/UpdatedDt` por registro, pero no una bitácora de acciones (quién hizo qué y cuándo, histórico de cambios). | Campos de auditoría por entidad; no hay tabla/servicio de bitácora. |
| F14 | Recuperación de contraseña / MFA | No hay "olvidé mi contraseña", restablecimiento ni segundo factor. | `AutenticationController` solo expone GetUsuarioByCorreo/Proveedor/Delete/SaveOrUpdate; no hay flujo de recuperación. |
| F15 | Notificaciones in-app reales | El panel de notificaciones del layout es estático (no hay datos ni backend de notificaciones). | `DespachoJuridicoDESIMVC/Views/Shared/_Layout.cshtml` (campana/panel sin datos); no hay entidad `Notificacion*`. |
| F16 | Reportes / exportación | No hay reportes (casos por estatus, carga por abogado, ingresos, vencimientos) ni exportación a PDF/Excel. | DataTables lista en pantalla; no hay endpoints de reporte ni librerías de exportación. |
| F17 | Catálogos adicionales | Solo existen Estatus de Caso y Tipos de Evento. Faltan catálogos de materia, instancia, tipo de parte, tipo de documento, moneda, etc. El proyecto `Catalog` está vacío. | `Catalog.Application/ICatalogApp.cs`, `Catalog.Proxy/ICatalogProxi.cs` (vacíos). |
| F18 | Portal del cliente | No existe acceso para que el cliente consulte su caso/expediente. | No hay vistas/controladores orientados al cliente; todo es back-office interno. |
| F19 | Paginación y búsqueda en servidor | Los listados traen todos los registros y paginan en el cliente (DataTables). No hay paginación en servidor. | 0 coincidencias de `Skip(`/`Take(`/`PageSize` en todo el código. |
| F20 | Versionado de base de datos | No hay scripts de creación/actualización de tablas ni de stored procedures en el repositorio; los SP viven solo en la base de datos. | 0 archivos `*.sql` en el repositorio; todas las llamadas son `CommandType.StoredProcedure` (`SqlProxy/DbWrapper.cs`). |

---

## 3. Deuda técnica (S1.2.2)

| # | Hallazgo | Evidencia (archivo:línea) | Impacto | Severidad |
|---|----------|---------------------------|---------|-----------|
| D1 | Código duplicado/muerto: `RecorController.cs` define la clase activa `RecordController` y está incluido en el `.csproj`; `RecordController.cs` (misma clase y `[RoutePrefix("api/Record")]`) está en disco pero **excluido** del `.csproj`. | `DespachoJuridicoDESIWebApi/Controllers/RecorController.cs:21` (`class RecordController`); `DespachoJuridicoDESIWebApi/Controllers/RecordController.cs` (no listado en `DespachoJuridicoDESIWebApi.csproj`; el `.csproj` incluye `Controllers\RecorController.cs`) | Confusión de mantenimiento; dos fuentes de verdad para el mismo controlador/ruta. | Alta |
| D2 | Inconsistencia de modelo: `RecordEntity` (en `Record.Messages`) vs `RecordObj` (en `Record.Domain`) para la misma entidad, y `IRecordApp.SaveOrUpdateRecord` recibe `RecordEntity`. | `Record.Messages/RecordMessages.cs:42` (`class RecordEntity`); `Record.Domain/RecordObj.cs` (`class RecordObj`); `Record.Application/IRecordApp.cs:18` | Duplicación de contratos; riesgo de divergencia y mapeos frágiles. | Media |
| D3 | Endpoints sin autorización: `[Authorize]` comentado en 4 controladores de la Web API. | `DespachoJuridicoDESIWebApi/Controllers/CaseController.cs:14`, `AgendaController.cs:17`, `DashboardController.cs:17`, `EventTypeController.cs:14` (comentado). Activo en `CourtController`, `CustomerController`, `RecorController` y en métodos de `AutenticationController`. | Acceso no autenticado a datos sensibles del despacho. | Crítica |
| D4 | Autenticación con hash débil: la contraseña se "cifra" con AES (`Cryptography.Encrypt`) usando `SaltKey`/`VIKey` **hardcodeados**, y se compara por igualdad de cadena. No es un hash con salt por usuario; el "salt" es una constante compartida. | `DespachoJuridicoDESIMVC/Helpers/CryptographyHelper.cs:14` (`SaltKey = "S@LT&KEY"`), `:21`, `:48`; `DespachoJuridicoDESIMVC/Controllers/HomeController.cs:46-47` (`Cryptography.Encrypt(pass)` y comparación `==`); `User.Application/UserApp.cs:109-116` (`x.PasswordHash.Equals(pass)`) | Almacenamiento reversible y secreto hardcodeado; riesgo de seguridad. | Crítica |
| D5 | Bug funcional: `RecordProxy.DeleteRecord` ejecuta el SP `DeleteExpediente` pero **ignora su resultado** y siempre devuelve `DateTime.Now`; no propaga éxito/error ni la fecha real devuelta por el SP. | `Record.Proxy/RecordProxy.cs:47-55` (`DataTable dt = GetObject("DeleteExpediente", ...); return DateTime.Now;`) | El cliente recibe éxito aunque el SP falle; no se distingue borrado real de error. | Alta |
| D6 | Consultas N+1 en listados: por cada elemento se consultan sus relaciones (cliente/estatus en Casos; caso/tipo de evento en Agenda). | `Case.Application/CaseApp.cs:121,135,196`; `Agenda.Application/AgendaApp.cs:88-90,140-142,166-168,229` (`PopulateRelatedObjects`) | Degradación de rendimiento al crecer los datos. | Media |
| D7 | Ausencia total de pruebas automatizadas. | No hay proyecto/carpeta de tests (0 `.csproj` de test); solución `DespachoJuridicoDESI.sln` sin proyecto de test. | Sin red de seguridad ante regresiones. | Alta |
| D8 | Sin paginación en servidor. | 0 coincidencias de `Skip(`/`Take(`/`PageSize`/`PageNumber` en el código. | Escalabilidad y tiempos de respuesta pobres. | Media |
| D9 | Sin validación centralizada. | Cada `*App` valida de forma ad-hoc devolviendo `OperationResult`; no hay capa de validación ni `ModelState`/FluentValidation. | Datos inconsistentes y duplicación de reglas. | Media |
| D10 | Proyecto `Catalog` vacío. | `Catalog.Application/ICatalogApp.cs`, `Catalog.Proxy/ICatalogProxi.cs`, `Catalog.Proxy/CatalogProxy.cs` sin miembros. | Ruido/estructura muerta; indica funcionalidad planeada y no implementada. | Baja |
| D11 | Errores de nomenclatura/estructura: carpeta `Agenda.Messagess` (mal escrita) y `CourtMessages` en lugar de `Court.Messages`; `CaseObj .cs` con espacio en el nombre. | `Agenda.Messagess/`; `CourtMessages/`; `Case.Domain/CaseObj .cs` | Mantenibilidad y consistencia. | Baja |
| D12 | Esquema de BD no versionado (sin scripts SQL de tablas ni SP). | 0 archivos `*.sql` en el repositorio. | Imposible reproducir/desplegar el entorno desde el código; dependencia de una BD existente. | Alta |
| D13 | Hash/secretos y cadena de conexión en configuración sin gestión de secretos. | `SqlProxy/DbWrapper.cs:18` (`ConfigurationManager.ConnectionStrings["cCon"]`); `CryptographyHelper.cs:14` (claves hardcodeadas). | Riesgo de filtración de credenciales. | Media |
| D14 | **Eliminación de Casos no implementada/rota**: la vista de caso enlaza a `/Caso/Delete` (controlador mal escrito "Caso") y `CaseController` (MVC) no define la acción `Delete`. | `DespachoJuridicoDESIMVC/Views/Case/Create.cshtml:188` (`@Url.Action("Delete", "Caso")`); `DespachoJuridicoDESIMVC/Controllers/CaseController.cs` (solo `Create`, `Index`, `GetAllCases`, `SaveOrUpdateCase`). | La acción de eliminar caso produce 404/acción inexistente. | Media |
| D15 | **Exposición del hash de contraseña**: el endpoint de login `GetUsuarioByCorreo` está marcado `[AllowAnonymous]` y devuelve `UserObjs`, que incluye `PasswordHash`, a un llamador no autenticado. | `DespachoJuridicoDESIWebApi/Controllers/AutenticationController.cs:21-31`; `User.Messages/UserMassagesResponse.cs:13` (`List<UserObj> UserObjs`); `User.Domain/UserObj.cs` (`PasswordHash`). | Fuga de credenciales; facilita ataques de fuerza bruta/offline. | Crítica |
| D16 | **`EmailHelper` re-lanza perdiendo el stack trace y fuerza SSL**: el `catch` hace `throw ex;` y el envío ignora el parámetro `ssl`, fijando `EnableSsl = true`. | `DespachoJuridicoDESIMVC/Helpers/EmailHelper.cs:13` (parámetro `ssl`), `:69` (`smtp.EnableSsl = true;`), `:83-86` (`catch (Exception ex) { throw ex; }`). | Dificulta el diagnóstico de fallos de correo; comportamiento SSL inflexible. | Baja |
| D17 | **`using static` autorreferente en `CatalogProxy`**: la clase vacía importa estáticamente su propio tipo. | `Catalog.Proxy/CatalogProxy.cs:9` (`using static Catalog.Proxy.CatalogProxy;`). | Código sin sentido/mantenimiento (módulo esqueleto). | Baja |

---

## 4. Conclusión del análisis

Para que la solución sea **funcional para un despacho jurídico** se requiere, como mínimo, cubrir las áreas F1–F20 (proceso sustantivo: actuaciones, partes, materia/instancia, plazos, tareas/responsables, documental, finanzas/CFDI, roles/permisos, auditoría, notificaciones, reportes y portal del cliente), y resolver prioritariamente la deuda técnica crítica **D3** (endpoints sin autorización), **D4** (cifrado reversible con claves hardcodeadas), **D15** (exposición del hash de contraseña), **D5** (bug de borrado de expediente) y **D7** (ausencia de pruebas). Asimismo, **D14** (eliminación de casos no implementada) rompe un flujo CRUD esperado.

> Documento de análisis; no se modificó código de la aplicación.
