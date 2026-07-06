using Case.Application;
using Case.Domain;
using Case.Proxy;
using Dashboard.Application;
using Dashboard.Messages;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Case.Messages.CaseMessages;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IDashboardApp _dashboardApp;

        public DashboardController(IDashboardApp dashboardApp)
        {
            _dashboardApp = dashboardApp;
        }

        /// <summary>
        /// Obtiene todos los datos del dashboard (resumen, eventos y actividad reciente)
        /// </summary>
        [HttpPost]
        [Route("GetDashboardData")]
        [SwaggerResponse(HttpStatusCode.OK, "Datos completos del dashboard", typeof(DashboardMessagesResponse))]
        public IHttpActionResult GetDashboardData()
        {
            DashboardMessagesResponse response = new DashboardMessagesResponse();
            response.Dashboard = _dashboardApp.GetDashboardData(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
