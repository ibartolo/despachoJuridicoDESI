using DespachoJuridicoDESIMVC.Models.Court;
using DespachoJuridicoDESIMVC.Models.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static DespachoJuridicoDESIMVC.Helpers.FiltersHelper;

namespace DespachoJuridicoDESIMVC.Controllers
{
    [Autenticated]
    public class CourtController : BaseController
    {
        #region Views

        public async Task<ActionResult> Create(long id = 0)
        {
            var courtObj = new CourtObj();

            if (id != 0)
            {
                var responseCourt = await httpClient.GetCourtById(id);
                if (responseCourt.Result.Successful)
                {
                    courtObj = responseCourt.CourtObjs.FirstOrDefault();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener la información del juzgado.");
                    return View(courtObj);
                }
            }

            return View(courtObj);
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        #endregion

        #region Data Access

        public async Task<string> GetAllCourts()
        {
            var response = await httpClient.GetAllCourts();
            return JsonConvert.SerializeObject(response);
        }

        public async Task<string> GetCourtById(long id)
        {
            var response = await httpClient.GetCourtById(id);
            return JsonConvert.SerializeObject(response);
        }

        public async Task<ActionResult> SaveOrUpdateCourt(CourtObj courtObj)
        {
            if (courtObj == null)
            {
                ModelState.AddModelError(string.Empty, "Datos del juzgado inválidos.");
                return View("Create");
            }

            try
            {
                var response = await httpClient.SaveOrUpdateCourt(courtObj);

                if (response?.Result?.Successful == true)
                {
                    return RedirectToAction("Index");
                }

                if (response?.Result?.SystemMessages != null && response.Result.SystemMessages.Any())
                {
                    foreach (var msg in response.Result.SystemMessages)
                    {
                        ModelState.AddModelError(string.Empty, msg.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error al guardar el juzgado: {ex.Message}");
            }

            return View("Create", courtObj);
        }

        public async Task<JsonResult> DeleteCourt(long id)
        {
            try
            {
                var response = await httpClient.DeleteCourt(id);

                if (response?.Result?.Successful == true)
                {
                    return Json(new { success = true, message = "Juzgado eliminado correctamente" });
                }

                var errorMessage = response?.Result?.SystemMessages?.FirstOrDefault()?.Message ?? "Error al eliminar el juzgado";
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