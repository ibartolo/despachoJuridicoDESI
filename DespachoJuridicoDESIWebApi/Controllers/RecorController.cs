using Case.Application;
using Case.Domain;
using Case.Proxy;
using Court.Domain;
using Record.Application;
using Record.Domain;
using Record.Messages;
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
    [Authorize]
    [RoutePrefix("api/Record")]
    public class RecordController : ApiController
    {
        private readonly IRecordApp _recordApp;

        public RecordController(IRecordApp recordApp)
        {
            _recordApp = recordApp;
        }

        /// <summary>
        /// Obtiene todos los expedientes activos
        /// </summary>
        [HttpPost]
        [Route("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "Lista de todos los expedientes", typeof(RecordMessagesResponse))]
        public IHttpActionResult GetAll()
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            response.RecordObjs = _recordApp.GetAllRecords(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        /// <summary>
        /// Obtiene expedientes por ID de caso
        /// </summary>
        [HttpPost]
        [Route("GetByCaseId")]
        [SwaggerResponse(HttpStatusCode.OK, "Lista de expedientes por caso", typeof(RecordMessagesResponse))]
        public IHttpActionResult GetByCaseId(GetRecordByCaseIdRequest request)
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            response.RecordObjs = _recordApp.GetRecordsByCaseId(request.CaseId, out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un expediente por su ID
        /// </summary>
        [HttpPost]
        [Route("Id")]
        [SwaggerResponse(HttpStatusCode.OK, "Expediente obtenido por ID", typeof(RecordMessagesResponse))]
        public IHttpActionResult GetById(GetRecordByIdRequest request)
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            response.RecordObjs = new List<RecordObj>();
            var item = _recordApp.GetRecordById(request.Id, out var operationResult);
            if (item != null)
                response.RecordObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }

        /// <summary>
        /// Elimina un expediente (cambia su estatus a 0)
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [SwaggerResponse(HttpStatusCode.OK, "Fecha de eliminación del expediente", typeof(object))]
        public IHttpActionResult Delete(DeleteRecordRequest request)
        {
            var response = new
            {
                DeletedDt = _recordApp.DeleteRecord(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        /// <summary>
        /// Guarda o actualiza un expediente (Si Id=0 crea, si Id>0 actualiza)
        /// </summary>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Expediente guardado o actualizado", typeof(RecordMessagesResponse))]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateRecordRequest request)
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            var item = _recordApp.SaveOrUpdateRecord(request.Record, out var operationResult);
            if (item != null)
                response.RecordObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
