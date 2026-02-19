using Customer.Application;
using Customer.Domain;
using Customer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly IClientApp _clientApp;

        public CustomerController(IClientApp clientApp)
        {
            _clientApp = clientApp;
        }

        [HttpPost]
        [Route("List")]
        public IHttpActionResult GetAllClientes()
        {
            ClientMassagesResponse response = new ClientMassagesResponse();
            response.ClientObjs = _clientApp.GetAllClientes(out var operationResult);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpPost]
        [Route("First")]
        public IHttpActionResult GetClienteById(GetClienteByIdRequest request)
        {
            ClientMassagesResponse response = new ClientMassagesResponse();
            response.ClientObjs = new List<ClientObj>();
            var cliente = _clientApp.GetClienteById(request.Id, out var operationResult);
            if (cliente != null)
                response.ClientObjs.Add(cliente);
            response.Result = operationResult;
            return Ok(response);
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteCliente(DeleteClienteRequest request)
        {
            var response = new
            {
                DeletedDt = _clientApp.DeleteCliente(request.Id, out var operationResult),
                Result = operationResult
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult SaveOrUpdateCliente([FromBody] SaveOrUpdateClienteRequest request)
        {
            ClientMassagesResponse response = new ClientMassagesResponse();
            response.ClientObjs = new List<ClientObj>();
            var cliente = _clientApp.SaveOrUpdateCliente(request.Cliente, out var operationResult);
            if (cliente != null)
                response.ClientObjs.Add(cliente);
            response.Result = operationResult;
            return Ok(response);
        }
    }
}
