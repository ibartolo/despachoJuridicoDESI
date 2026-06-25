using DespachoJuridicoDESIMVC.Models.Case;
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
    public class CaseController : BaseController
    {
        #region Views
        public async Task<ActionResult> Create(long id = 0)
        {

            var caseObj = new CaseObj();

            if (id != 0)
            { 
                var responseCase = await httpClient.GetCaseById(id);
                if (responseCase.Result.Successful)
                {
                    caseObj = responseCase.CaseObjs.FirstOrDefault();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener la información del caso.");
                    return View(caseObj);
                }
            }

            var response = await httpClient.GetAllClientes();

            var listItemsCleintes = new List<SelectListItem>();
            var listItemsEstatus = new List<SelectListItem>();

            foreach (var i in response.ClientObjs)
            {
                listItemsCleintes.Add(new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });
            }

            var responseStatusCase = await httpClient.GetAllStatusCase();
            var listStatusCase = new List<StatusCaseobj>();

            if (responseStatusCase.Result.Successful)
            {
                listStatusCase.AddRange(responseStatusCase.StatusCaseObjs);
                foreach (var i in listStatusCase)
                {
                    listItemsEstatus.Add(new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Id.ToString()
                    });
                }
            }

            ViewBag.EstatusCaso = listItemsEstatus;
            ViewBag.Clientes = listItemsCleintes;
            return View(caseObj);
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
        #endregion

        #region Data Access
        public async Task<string> GetAllCases()
        {
            var response = await httpClient.GetAllCase();
            return JsonConvert.SerializeObject(response);
        }
        public async Task<ActionResult> SaveOrUpdateCase(CaseObj caseObj)
        {
            if (caseObj == null)
            {
                ModelState.AddModelError(string.Empty, "Datos de caso inválidos.");
                return View("Create");
            }
            try
            {
                var response = await httpClient.SaveOrUpdateCase(caseObj);
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
                ModelState.AddModelError(string.Empty, $"Ocurrió un error al guardar el caso: {ex.Message}");
            }
            return View("Create", caseObj);
        }
        #endregion
    }
}