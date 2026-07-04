using DespachoJuridicoDESIMVC.Models.Agenda;
using DespachoJuridicoDESIMVC.Models.Common;
using DespachoJuridicoDESIMVC.Models.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static DespachoJuridicoDESIMVC.Helpers.FiltersHelper;

namespace DespachoJuridicoDESIMVC.Controllers
{
    [Autenticated]
    public class CalendarController : BaseController
    {
        #region Views

        public async Task<ActionResult> Index()
        {
            return View();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Obtiene todos los eventos para el calendario
        /// </summary>
        public async Task<string> GetAllEvents()
        {
            var items = new List<object>();

            var response = await httpClient.GetAllAgenda();

            if (response?.Result?.Successful == true && response.AgendaItems != null)
            {
                foreach (var agenda in response.AgendaItems)
                {
                    // Construir título con información del caso y tipo de evento
                    var titulo = agenda.Titulo;

                    // Si no hay título, usar el nombre del caso o tipo de evento
                    if (string.IsNullOrEmpty(titulo))
                    {
                        if (!string.IsNullOrEmpty(agenda.NombreCaso))
                            titulo = agenda.NombreCaso;
                        else if (!string.IsNullOrEmpty(agenda.NombreTipoEvento))
                            titulo = agenda.NombreTipoEvento;
                        else
                            titulo = "Evento sin título";
                    }

                    var item = new
                    {
                        id = agenda.Id,
                        title = titulo,
                        start = agenda.FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end = agenda.FechaFin?.ToString("yyyy-MM-ddTHH:mm:ss"),
                        color = ObtenerColorPorTipoEvento(agenda.TipoEventoId),
                        extendedProps = new
                        {
                            casoId = agenda.CasoId,
                            tipoEventoId = agenda.TipoEventoId,
                            descripcion = agenda.Descripcion
                        }
                    };

                    items.Add(item);
                }
            }

            var result = new
            {
                Result = new OperationResult { Successful = true },
                Events = items
            };

            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// Asigna un color según el tipo de evento
        /// </summary>
        private string ObtenerColorPorTipoEvento(long tipoEventoId)
        {
            // Puedes personalizar los colores según tus tipos de evento
            // O obtenerlos desde un catálogo si está disponible
            var colores = new Dictionary<long, string>
    {
        { 1, "#3498db" },  // Audiencia
        { 2, "#2ecc71" },  // Reunión
        { 3, "#f39c12" },  // Entrega de documentos
        { 4, "#e74c3c" },  // Juicio
        { 5, "#9b59b6" },  // Revisión de expediente
        { 6, "#1abc9c" },  // Cita con cliente
        { 7, "#2c3e50" },  // Firma de contrato
    };

            return colores.ContainsKey(tipoEventoId) ? colores[tipoEventoId] : "#95a5a6";
        }

        /// <summary>
        /// Obtiene casos para el DDL
        /// </summary>
        public async Task<string> GetCases()
        {
            var response = await httpClient.GetAllCase();
            return JsonConvert.SerializeObject(response);
        }

        /// <summary>
        /// Obtiene tipos de evento para el DDL
        /// </summary>
        public async Task<string> GetEventTypes()
        {
            var response = await httpClient.GetAllEventTypes();
            return JsonConvert.SerializeObject(response);
        }

        /// <summary>
        /// Guarda o actualiza agenda
        /// </summary>
        public async Task<JsonResult> SaveOrUpdateAgenda(AgendaObj agenda)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(agenda.Titulo))
                {
                    return Json(new { success = false, message = "El título es obligatorio." });
                }

                if (agenda.CasoId <= 0)
                {
                    return Json(new { success = false, message = "Debe seleccionar un caso." });
                }

                if (agenda.TipoEventoId <= 0)
                {
                    return Json(new { success = false, message = "Debe seleccionar un tipo de evento." });
                }

                if (agenda.FechaInicio == DateTime.MinValue)
                {
                    return Json(new { success = false, message = "La fecha de inicio es obligatoria." });
                }

                var response = await httpClient.SaveOrUpdateAgenda(agenda);

                if (response?.Result?.Successful == true)
                {
                    return Json(new { success = true, message = "Cita guardada correctamente." });
                }

                var errorMessage = response?.Result?.SystemMessages?.FirstOrDefault()?.Message ?? "Error al guardar la cita.";
                return Json(new { success = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Elimina agenda
        /// </summary>
        public async Task<JsonResult> DeleteAgenda(long id)
        {
            try
            {
                var response = await httpClient.DeleteAgenda(id);

                if (response?.Result?.Successful == true)
                {
                    return Json(new { success = true, message = "Cita eliminada correctamente." });
                }

                var errorMessage = response?.Result?.SystemMessages?.FirstOrDefault()?.Message ?? "Error al eliminar la cita.";
                return Json(new { success = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        #endregion
    }
}