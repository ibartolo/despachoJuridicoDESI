using DespachoJuridicoDESIMVC.Models.Case;
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
        // GET: Case
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
            listItemsEstatus.Add(new SelectListItem
            {
                Text = "Abierto",
                Value = "1"
            });
            listItemsEstatus.Add(new SelectListItem
            {
                Text = "Cerrado",
                Value = "2"
            });


            ViewBag.EstatusCaso = listItemsEstatus;
            ViewBag.Clientes = listItemsCleintes;
            return View(new CaseObj());
        }
    }
}