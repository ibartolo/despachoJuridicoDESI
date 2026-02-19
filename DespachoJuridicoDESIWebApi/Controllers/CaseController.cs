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
    [RoutePrefix("api/Case")]
    public class CaseController : ApiController
    {
        private readonly ICaseApp _caseApp;
        public CaseController(ICaseApp caseApp)
        {
            _caseApp = caseApp;
        }

        [HttpPost]
        [Route("StatusCase/List")]
        public IHttpActionResult GetAll()
        {
            StatusCaseMessagesResponse response = new StatusCaseMessagesResponse();
            response.StatusCaseObjs = _caseApp.GetAllStatusCase(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("StatusCase/First")]
        public IHttpActionResult GetById(GetStatusCaseByIdRequest request)
        {
            StatusCaseMessagesResponse response = new StatusCaseMessagesResponse();
            response.StatusCaseObjs = new List<StatusCaseObj>();
            var item = _caseApp.GetStatusCaseById(request.Id, out var operationResult);
            if (item != null)
                response.StatusCaseObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpDelete]
        [Route("StatusCase")]
        public IHttpActionResult Delete(DeleteStatusCaseRequest request)
        {
            var response = new
            {
                DeletedDt = _caseApp.DeleteStatusCase(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("StatusCase")]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateStatusCaseRequest request)
        {
            StatusCaseMessagesResponse response = new StatusCaseMessagesResponse();
            response.StatusCaseObjs = new List<StatusCaseObj>();
            var item = _caseApp.SaveOrUpdateStatusCase(request.StatusCase, out var operationResult);
            if (item != null)
                response.StatusCaseObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
