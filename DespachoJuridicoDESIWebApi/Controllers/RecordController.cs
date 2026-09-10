using Case.Application;
using Case.Domain;
using Case.Proxy;
using Court.Application;
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
using static Record.Messages.RecordMessages;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    /// <summary>
    /// Controlador API para la gestión de Expedientes (Records).
    /// Proporciona operaciones CRUD (Create, Read, Update, Delete) para registros de expedientes.
    /// </summary>
    /// <remarks>
    /// Todos los endpoints de este controlador se encuentran bajo la ruta base: /api/Record
    /// Afecta la tabla: Expediente
    /// Un expediente está asociado a un caso y a un juzgado específico.
    /// </remarks>
    [Authorize]
    [RoutePrefix("api/Record")]
    public class RecordController : ApiController
    {
        private readonly IRecordApp _recordApp;

        /// <summary>
        /// Constructor del controlador de Expedientes.
        /// </summary>
        /// <param name="recordApp">Servicio de aplicación para operaciones con expedientes (inyección de dependencias).</param>
        public RecordController(IRecordApp recordApp)
        {
            _recordApp = recordApp;
        }

        /// <summary>
        /// Obtiene todos los expedientes asociados a un caso específico.
        /// </summary>
        /// <remarks>
        /// Método HTTP: POST
        /// Ruta completa: POST /api/Record/GetByCaseId
        /// Retorna todos los expedientes que pertenecen a un caso determinado.
        /// </remarks>
        /// <param name="request">Objeto GetRecordByCaseIdRequest con el Id del caso.</param>
        /// <returns>
        /// Retorna un objeto RecordMessagesResponse que contiene:
        /// - RecordObjs: Lista de objetos RecordObj con los expedientes del caso
        /// - Result: Objeto OperationResult con el estado de la operación (Successful, SystemMessages)
        /// </returns>
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
        /// Obtiene un expediente específico por su identificador.
        /// </summary>
        /// <remarks>
        /// Método HTTP: POST
        /// Ruta completa: POST /api/Record/GetById
        /// </remarks>
        /// <param name="request">Objeto GetRecordByIdRequest con el Id del expediente a obtener.</param>
        /// <returns>
        /// Retorna un objeto RecordMessagesResponse con el expediente encontrado (si existe) 
        /// y el resultado de la operación.
        /// </returns>
        [HttpPost]
        [Route("GetById")]
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
        /// Elimina un expediente del sistema.
        /// </summary>
        /// <remarks>
        /// Método HTTP: POST
        /// Ruta completa: POST /api/Record/Delete
        /// </remarks>
        /// <param name="request">Objeto DeleteRecordRequest con el Id del expediente a eliminar.</param>
        /// <returns>
        /// Retorna un objeto anónimo con:
        /// - DeletedDt: Fecha y hora de eliminación
        /// - Result: Objeto OperationResult con el estado de la operación
        /// </returns>
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
        /// Guarda un nuevo expediente o actualiza uno existente.
        /// </summary>
        /// <remarks>
        /// Método HTTP: POST
        /// Ruta completa: POST /api/Record/SaveOrUpdate
        /// - Para crear un nuevo registro: envía Id = 0
        /// - Para actualizar: envía el Id del registro existente
        /// 
        /// El expediente debe estar asociado a un caso y a un juzgado válidos.
        /// </remarks>
        /// <param name="request">Objeto SaveOrUpdateRecordRequest que contiene el RecordObj a guardar o actualizar.</param>
        /// <returns>
        /// Retorna un objeto RecordMessagesResponse con el expediente guardado/actualizado
        /// y el resultado de la operación.
        /// </returns>
        [HttpPost]
        [Route("SaveOrUpdate")]
        [SwaggerResponse(HttpStatusCode.OK, "Expediente guardado o actualizado", typeof(RecordMessagesResponse))]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateRecordRequest request)
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            response.RecordObjs = new List<RecordObj>();
            var item = _recordApp.SaveOrUpdateRecord(request.Record, out var operationResult);
            if (item != null)
                response.RecordObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
