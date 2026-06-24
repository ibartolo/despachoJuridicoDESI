using Case.Application;
using Case.Domain;
using Case.Proxy;
using Record.Application;
using Record.Domain;
using Record.Messages;
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
    [RoutePrefix("api/Record")]
    public class RecordController : ApiController
    {
        private readonly IRecordApp _recordApp;

        public RecordController(IRecordApp recordApp)
        {
            _recordApp = recordApp;
        }

        [HttpPost]
        [Route("GetByCaseId")]
        public IHttpActionResult GetByCaseId(GetRecordByCaseIdRequest request)
        {
            RecordMessagesResponse response = new RecordMessagesResponse();
            response.RecordObjs = _recordApp.GetRecordsByCaseId(request.CaseId, out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("GetById")]
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

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(DeleteRecordRequest request)
        {
            var response = new
            {
                DeletedDt = _recordApp.DeleteRecord(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdate")]
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
