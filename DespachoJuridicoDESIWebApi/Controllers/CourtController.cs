using Case.Application;
using Case.Domain;
using Case.Proxy;
using Court.Application;
using Court.Domain;
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
using static Court.Messages.CourtMessages;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Court")]
    public class CourtController : ApiController
    {
        private readonly ICourtApp _courtApp;

        public CourtController(ICourtApp courtApp)
        {
            _courtApp = courtApp;
        }

        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            CourtMessagesResponse response = new CourtMessagesResponse();
            response.CourtObjs = _courtApp.GetAllCourts(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("GetById")]
        public IHttpActionResult GetById(GetCourtByIdRequest request)
        {
            CourtMessagesResponse response = new CourtMessagesResponse();
            response.CourtObjs = new List<CourtObj>();
            var item = _courtApp.GetCourtById(request.Id, out var operationResult);
            if (item != null)
                response.CourtObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(DeleteCourtRequest request)
        {
            var response = new
            {
                DeletedDt = _courtApp.DeleteCourt(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdate")]
        public IHttpActionResult SaveOrUpdate(SaveOrUpdateCourtRequest request)
        {
            CourtMessagesResponse response = new CourtMessagesResponse();
            response.CourtObjs = new List<CourtObj>();
            var item = _courtApp.SaveOrUpdateCourt(request.Court, out var operationResult);
            if (item != null)
                response.CourtObjs.Add(item);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
