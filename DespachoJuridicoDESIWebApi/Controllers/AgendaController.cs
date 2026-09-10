using Agenda.Application;
using Agenda.Domain;
using Agenda.Messagess;
using Case.Application;
using Case.Domain;
using Case.Proxy;
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
    [RoutePrefix("api/Agenda")]
    public class AgendaController : ApiController
    {
        private readonly IAgendaApp _agendaApp;

        public AgendaController(IAgendaApp agendaApp)
        {
            _agendaApp = agendaApp;
        }

        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            AgendaMessagesResponse response = new AgendaMessagesResponse();
            response.AgendaItems = _agendaApp.GetAllAgenda(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("GetById")]
        public IHttpActionResult GetById(GetAgendaByIdRequest request)
        {
            AgendaMessagesResponse response = new AgendaMessagesResponse();
            response.AgendaItems = new List<AgendaEntity>();
            var item = _agendaApp.GetAgendaById(request.Id, out var operationResult);
            if (item != null)
                response.AgendaItems.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("GetByCasoId")]
        public IHttpActionResult GetByCasoId(GetAgendaByCasoIdRequest request)
        {
            AgendaMessagesResponse response = new AgendaMessagesResponse();
            response.AgendaItems = _agendaApp.GetAgendaByCasoId(request.CasoId, out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("GetByFecha")]
        public IHttpActionResult GetByFecha(GetAgendaByFechaRequest request)
        {
            AgendaMessagesResponse response = new AgendaMessagesResponse();
            response.AgendaItems = _agendaApp.GetAgendaByFecha(request.FechaInicio, request.FechaFin, out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(DeleteAgendaRequest request)
        {
            var response = new
            {
                DeletedDt = _agendaApp.DeleteAgenda(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdate")]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateAgendaRequest request)
        {
            AgendaMessagesResponse response = new AgendaMessagesResponse();
            response.AgendaItems = new List<AgendaEntity>();
            var item = _agendaApp.SaveOrUpdateAgenda(request.Agenda, out var operationResult);
            if (item != null)
                response.AgendaItems.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }

}
