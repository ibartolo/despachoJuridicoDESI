using DespachoJuridicoDESIMVC.DAL;
using DespachoJuridicoDESIMVC.Helpers;
using DespachoJuridicoDESIMVC.Models.Autentication;
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
    public class CustomerController : BaseController
    {
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region Data Access
        public async Task<string> GetAllClientes()
        {
            var response = await httpClient.GetAllClientes();

            return JsonConvert.SerializeObject(response);
        }
        #endregion
    }
}