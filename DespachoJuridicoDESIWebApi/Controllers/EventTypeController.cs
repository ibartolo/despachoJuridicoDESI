using EventType.Application;
using EventType.Domain;
using EventType.Messages;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/EventType")]
    public class EventTypeController : ApiController
    {
        private readonly IEventTypeApp _eventTypeApp;

        public EventTypeController(IEventTypeApp eventTypeApp)
        {
            _eventTypeApp = eventTypeApp;
        }

        /// <summary>
        /// Obtiene todos los tipos de evento activos
        /// </summary>
        [HttpPost]
        [Route("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "Lista de todos los tipos de evento", typeof(EventTypeMessagesResponse))]
        public IHttpActionResult GetAll()
        {
            EventTypeMessagesResponse response = new EventTypeMessagesResponse();
            response.EventTypeObjs = _eventTypeApp.GetAllEventTypes(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de evento por su ID
        /// </summary>
        [HttpPost]
        [Route("GetById")]
        [SwaggerResponse(HttpStatusCode.OK, "Tipo de evento obtenido por ID", typeof(EventTypeMessagesResponse))]
        public IHttpActionResult GetById(GetEventTypeByIdRequest request)
        {
            EventTypeMessagesResponse response = new EventTypeMessagesResponse();
            response.EventTypeObjs = new List<EventTypeObj>();
            var item = _eventTypeApp.GetEventTypeById(request.Id, out var operationResult);
            if (item != null)
                response.EventTypeObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de evento (cambia su estatus a 0)
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [SwaggerResponse(HttpStatusCode.OK, "Fecha de eliminación del tipo de evento", typeof(object))]
        public IHttpActionResult Delete(DeleteEventTypeRequest request)
        {
            var response = new
            {
                DeletedDt = _eventTypeApp.DeleteEventType(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        /// <summary>
        /// Guarda o actualiza un tipo de evento (Si Id=0 crea, si Id>0 actualiza)
        /// </summary>
        [HttpPost]
        [Route("SaveOrUpdate")]
        [SwaggerResponse(HttpStatusCode.OK, "Tipo de evento guardado o actualizado", typeof(EventTypeMessagesResponse))]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateEventTypeRequest request)
        {
            EventTypeMessagesResponse response = new EventTypeMessagesResponse();
            response.EventTypeObjs = new List<EventTypeObj>();
            var item = _eventTypeApp.SaveOrUpdateEventType(request.EventType, out var operationResult);
            if (item != null)
                response.EventTypeObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
