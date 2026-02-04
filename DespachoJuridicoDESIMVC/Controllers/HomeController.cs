using DespachoJuridicoDESIMVC.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DespachoJuridicoDESIMVC.Controllers
{
    public class HomeController : Controller
    {
        private HttpClientConnection httpClient;
        public HomeController()
        {
            httpClient = new HttpClientConnection();
        }
        #region Views
        public ActionResult Autentication()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Data Access
        public string AutenticacionUsuario(string email)
        {
            var response = httpClient.GetUsuarioByCorreo(email);
            return JsonConvert.SerializeObject(response);
        }
        #endregion
    }
}