using DespachoJuridicoDESIMVC.Models.Court;
using DespachoJuridicoDESIMVC.Models.Record;
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
    public class RecordController : BaseController
    {
        #region Views

        public async Task<ActionResult> Create(long id = 0)
        {
            var recordObj = new RecordObj();

            if (id != 0)
            {
                var responseRecord = await httpClient.GetRecordById(id);
                if (responseRecord.Result.Successful)
                {
                    recordObj = responseRecord.RecordObjs.FirstOrDefault();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener la información del expediente.");
                    return View(recordObj);
                }
            }

            // Cargar lista de casos para el DDL
            var responseCases = await httpClient.GetAllCase();
            var listItemsCases = new List<SelectListItem>();

            if (responseCases.Result.Successful)
            {
                foreach (var caseObj in responseCases.CaseObjs)
                {
                    listItemsCases.Add(new SelectListItem
                    {
                        Text = caseObj.NumeroCaso,
                        Value = caseObj.Id.ToString()
                    });
                }
            }

            // Cargar lista de juzgados para el DDL
            var responseCourts = await httpClient.GetAllCourts();
            var listItemsCourts = new List<SelectListItem>();

            if (responseCourts.Result.Successful)
            {
                foreach (var court in responseCourts.CourtObjs)
                {
                    listItemsCourts.Add(new SelectListItem
                    {
                        Text = court.Nombre,
                        Value = court.Id.ToString()
                    });
                }
            }

            ViewBag.Cases = listItemsCases;
            ViewBag.Courts = listItemsCourts;

            return View(recordObj);
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        #endregion

        #region Data Access

        public async Task<string> GetAllRecords()
        {
            var response = await httpClient.GetAllRecords();
            return JsonConvert.SerializeObject(response);
        }

        public async Task<string> GetRecordById(long id)
        {
            var response = await httpClient.GetRecordById(id);
            return JsonConvert.SerializeObject(response);
        }

        public async Task<ActionResult> SaveOrUpdateRecord(RecordObj recordObj)
        {
            if (recordObj == null)
            {
                ModelState.AddModelError(string.Empty, "Datos del expediente inválidos.");
                return View("Create");
            }

            try
            {
                var response = await httpClient.SaveOrUpdateRecord(recordObj);

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
                ModelState.AddModelError(string.Empty, $"Ocurrió un error al guardar el expediente: {ex.Message}");
            }

            // Recargar datos para los ViewBag en caso de error
            var responseCases = await httpClient.GetAllCase();
            var listItemsCases = new List<SelectListItem>();

            if (responseCases.Result.Successful)
            {
                foreach (var caseObj in responseCases.CaseObjs)
                {
                    listItemsCases.Add(new SelectListItem
                    {
                        Text = caseObj.NumeroCaso,
                        Value = caseObj.Id.ToString()
                    });
                }
            }

            var responseCourts = await httpClient.GetAllCourts();
            var listItemsCourts = new List<SelectListItem>();

            if (responseCourts.Result.Successful)
            {
                foreach (var court in responseCourts.CourtObjs)
                {
                    listItemsCourts.Add(new SelectListItem
                    {
                        Text = court.Nombre,
                        Value = court.Id.ToString()
                    });
                }
            }

            ViewBag.Cases = listItemsCases;
            ViewBag.Courts = listItemsCourts;

            return View("Create", recordObj);
        }

        public async Task<JsonResult> DeleteRecord(long id)
        {
            try
            {
                var response = await httpClient.DeleteRecord(id);

                if (response?.Result?.Successful == true)
                {
                    return Json(new { success = true, message = "Expediente eliminado correctamente" });
                }

                var errorMessage = response?.Result?.SystemMessages?.FirstOrDefault()?.Message ?? "Error al eliminar el expediente";
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