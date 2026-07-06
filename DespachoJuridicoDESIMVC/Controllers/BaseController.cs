using DespachoJuridicoDESIMVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace DespachoJuridicoDESIMVC.Controllers
{
    public class BaseController : Controller
    {
        public HttpClientConnection httpClient;

        public BaseController()
        {
            httpClient = new HttpClientConnection();
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                // Expresión regular para validar formato de correo electrónico
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }
    }
}