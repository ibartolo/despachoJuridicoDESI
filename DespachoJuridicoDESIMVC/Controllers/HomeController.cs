using DespachoJuridicoDESIMVC.DAL;
using DespachoJuridicoDESIMVC.Helpers;
using DespachoJuridicoDESIMVC.Models.Autentication;
using DespachoJuridicoDESIMVC.Models.Dashboard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static DespachoJuridicoDESIMVC.Helpers.FiltersHelper;

namespace DespachoJuridicoDESIMVC.Controllers
{
    public class HomeController : BaseController
    {

        #region Views
        [NoAutenticated]
        public ActionResult Autentication()
        {
            return View();
        }
        [Autenticated]
        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetDashboardData();

            if (response?.Result?.Successful == true)
            {
                return View(response.Dashboard);
            }

            // En caso de error, mostrar dashboard vacío
            ViewBag.Error = response?.Result?.SystemMessages?.FirstOrDefault()?.Message ?? "Error al cargar el dashboard";
            return View(new DashboardObj());
        }
        #endregion

        #region Data Access
        public async Task<string> AutenticacionUsuario(string email, string pass)
        {
            var response = await httpClient.GetUsuarioByCorreo(email);

            var passEncrypt = Cryptography.Encrypt(pass);
            var usuarioAutenticado = response.UserObjs.Where(u => u.PasswordHash == passEncrypt).ToList();

            if (usuarioAutenticado.Count == 0)
            {
                response.Result.Successful = false;
                response.Result.SystemMessages.Add(new Models.Common.SystemMessage()
                {
                    Message = "Error de autenticación. Verifique sus credenciales e intente nuevamente.",
                    MessageType = Models.Common.SystemMessageTypes.Error
                });
            }
            else {
                var userAutenticated = usuarioAutenticado.First();
                var token = await new HttpClientConnection().GetToken(email, passEncrypt);
                token.ExpirationDate = DateTime.Now.AddSeconds(token.expires_in);

                var tokenCookie = new TokenCookie()
                {
                    Token = token,
                    UserID = userAutenticated.Id,
                    UserName = userAutenticated.Correo
                };

                SessionHelper.CreateSession(JsonConvert.SerializeObject(tokenCookie));
            }

                return JsonConvert.SerializeObject(response);
        }

        public ActionResult LogOut()
        {
            SessionHelper.CloseSession();
            if (Request.Cookies["ConfigMenu"] != null)
            {
                var c = new HttpCookie("ConfigMenu")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Autentication");
        }
        #endregion
    }
}