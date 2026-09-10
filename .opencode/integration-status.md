# Integration Status

## Verification Run
- Reviewer session: S2.1.1 — re-verificación completa e independiente (no se confió en los informes previos).
- Date: 2026-09-09
- Scope: S1.1.1, S1.1.2, S1.2.1, S1.2.2 y S2.1.1, contra el código real en `C:\Git\despachoJuridicoDESI`.
- Build/Test: N/A — misión SOLO de análisis. No hay proyecto de pruebas y no se puede compilar (.NET Framework 4.8 vs `dotnet` 9 disponible). Verificación = correspondencia documento ↔ código real (lectura directa + búsquedas).

## Result: PASS

| Subtask | Deliverable | Result | Evidence |
|---------|-------------|--------|----------|
| S1.1.1 | `.opencode/docs/inventario-funcionalidad.md` | PASS | 10 entidades de dominio verificadas campo a campo (`ClientObj`, `CaseObj`, `StatusCaseObj`, `RecordObj`, `CourtObj`, `AgendaObj`, `AgendaEntity`, `EventTypeObj`, `DashboardObj`, `UserObj`). `Catalog.*` vacío. |
| S1.1.2 | `.opencode/docs/inventario-funcionalidad.md` | PASS | 37 endpoints REST activos + `/token`; 30 acciones MVC en 7 controladores; 11 vistas; 3 plantillas de correo. |
| S1.2.1 | `.opencode/docs/funcionalidad-faltante.md` §2 | PASS | 20 brechas F1–F20 confirmadas por ausencia de entidades/términos y por los campos realmente existentes. |
| S1.2.2 | `.opencode/docs/funcionalidad-faltante.md` §3 | PASS | 17 hallazgos D1–D17 re-verificados 1:1 con `archivo:línea`. |
| S2.1.1 | `.opencode/todo.md` | PASS | Todos los subtasks marcados `[x]`; 0 pendientes. |
| Extra | `.opencode/docs/funcionalidad-faltante-y-deuda-tecnica.md` | PASS | Superset (20 deudas numeradas + F por áreas). Sin contradicciones con el documento canónico; hallazgos extra (CORS `AllowAll`, zona horaria fija, token 6 h, sin transacciones, `MapAuditFields`) también verificados. |

## Key evidence re-verified (archivo:línea)
- D1: `DespachoJuridicoDESIWebApi.csproj:166` incluye `Controllers\RecorController.cs`; `Controllers\RecordController.cs` NO aparece en el `.csproj` (grep en `*.csproj` solo devuelve `RecorController`). Ambas clases `RecordController` con `[RoutePrefix("api/Record")]` (`RecorController.cs:20-21`, `RecordController.cs:30-31`).
- D2: `Record.Messages/RecordMessages.cs:42` (`class RecordEntity`) vs `Record.Domain/RecordObj.cs:11` (`class RecordObj`); `Record.Application/IRecordApp.cs:18` recibe `RecordEntity`.
- D3: `//[Authorize]` en `CaseController.cs:14`, `AgendaController.cs:17`, `DashboardController.cs:17`, `EventTypeController.cs:14`. Activo en `CourtController.cs:26`, `CustomerController.cs:13`, `RecorController.cs:19`; `AutenticationController.cs:21` (`[AllowAnonymous]`) y `:32` (`[Authorize]`).
- D4: `DespachoJuridicoDESIMVC/Helpers/CryptographyHelper.cs:13-15` (claves `P@@Sw0rd`/`S@LT&KEY`/VI hardcodeadas, cifrado reversible); `HomeController.cs:46-47` (`Cryptography.Encrypt(pass)` + comparación `==`); `User.Application/UserApp.cs:116` (`x.PasswordHash.Equals(pass)`).
- D5: `Record.Proxy/RecordProxy.cs:47-55` (`DataTable dt = GetObject("DeleteExpediente", ...); return DateTime.Now;`).
- D6: `Case.Application/CaseApp.cs:125,135,167,200,264` (N+1 estatus/cliente); `Agenda.Application/AgendaApp.cs:59` (`PopulateRelatedObjects`) invocado en `:90,116,142,168,229`.
- D7/D8/D12/F19/F20: 0 proyectos de test en `.sln`/`*.csproj`; 0 coincidencias de `Skip(`/`Take(`/`PageSize`/`PageNumber`; 0 archivos `*.sql` (glob global).
- D10/F17: `CatalogApp`, `ICatalogApp` (internal), `CatalogProxy`, `ICatalogProxi`, `CatalogMapp` sin miembros.
- D11: `.sln` referencia `Agenda.Messagess` y `CourtMessages\Court.Messages.csproj`; `Case.Domain/CaseObj .cs` con espacio.
- D13: `SqlProxy/DbWrapper.cs:18` (`ConnectionStrings["cCon"]`).
- D14: `Views/Case/Create.cshtml:188` (`@Url.Action("Delete", "Caso")`); no existe `CasoController` (grep) y el MVC `CaseController` solo define `Create`, `Index`, `GetAllCases`, `SaveOrUpdateCase` (sin `Delete`).
- D15: `AutenticationController.cs:21-31` (`[AllowAnonymous]` devuelve `UserMassagesResponse.UserObjs` → `List<UserObj>` con `PasswordHash`).
- D16: `Helpers/EmailHelper.cs:13` (parámetro `ssl`), `:69` (`EnableSsl = true`), `:83-86` (`throw ex`).
- D17: `Catalog.Proxy/CatalogProxy.cs:9` (`using static Catalog.Proxy.CatalogProxy;` autorreferente).
- F5: flags `GeneraEvento`/`GeneraNotificacion` solo en `StatusCaseObj`, `StatusCaseEntity`, `StatusCaseMapp` y modelos MVC; sin consumidor en `*App`.
- F7: campos Google en `AgendaObj`/`AgendaEntity`, solo mapeados (`AgendaMapp.cs:65-78,117`) y persistidos (`AgendaProxy.cs:75-77`); sin cliente de API externa.
- F11: `Customer.Domain/ClientObj.cs` sin RFC/régimen/dirección fiscal.
- F15: `_Layout.cshtml:51` badge fijo `"3"`; panel estático.
- F16: menú `Pagos` (`:118`), `Reportes` (`:124`), `Configuración` (`:130`) en `_Layout.cshtml`.
- Extra (doc ses_3): `Startup.cs:85` (`UseCors(...AllowAll)`), `:95` (token 6 h), `:153` (`role=user` fijo); `SessionHelper.cs:79` (`Central Standard Time`).

## Sync Status
- Unresolved issues: 0. `sync-issues.md` = CLEAN. No se encontró ninguna discrepancia documento ↔ código; no se generó ningún SYNC-N nuevo.

## Notes
- Se mantienen los `[x]` de `.opencode/todo.md` (ya estaban correctos y se reconfirmaron).
- Observación de trazabilidad (no bloqueante): el conteo "6/6" en `status.md`/`context.md` no coincide con los 5 subtasks-hoja reales del `todo.md` (S1.1.1, S1.1.2, S1.2.1, S1.2.2, S2.1.1); corregido en `status.md`.
