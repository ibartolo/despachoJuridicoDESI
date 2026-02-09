using DespachoJuridicoDESIMVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}