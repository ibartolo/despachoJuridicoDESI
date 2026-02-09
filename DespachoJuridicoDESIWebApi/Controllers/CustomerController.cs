using Customer.Application;
using Customer.Domain;
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

        [HttpGet]
        [Route("GetAllClientes")]
        public IHttpActionResult GetAllClientes()
        {
            var result = _clientApp.GetAllClientes(out var operationResult);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetClienteById")]
        public IHttpActionResult GetClienteById(long id)
        {
            var result = _clientApp.GetClienteById(id, out var operationResult);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCliente")]
        public IHttpActionResult DeleteCliente(long id)
        {
            var result = _clientApp.DeleteCliente(id, out var operationResult);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveOrUpdateCliente")]
        public IHttpActionResult SaveOrUpdateCliente(ClientObj cliente)
        {
            var result = _clientApp.SaveOrUpdateCliente(cliente, out var operationResult);
            return Ok(result);
        }
    }
}
