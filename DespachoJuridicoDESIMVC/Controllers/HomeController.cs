using DespachoJuridicoDESIMVC.DAL;
using DespachoJuridicoDESIMVC.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return JsonConvert.SerializeObject(response);
        }
        #endregion
    }
}