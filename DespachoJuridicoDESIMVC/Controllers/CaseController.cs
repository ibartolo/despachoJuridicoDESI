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
        public async Task<ActionResult> Index()
        {
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
            return View(new CaseObj());
        }
        #endregion

        #region Data Access

        #endregion

        //SaveOrUpdateCaso
    }
}