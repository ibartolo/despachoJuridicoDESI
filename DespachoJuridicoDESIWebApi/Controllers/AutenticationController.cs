using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using User.Application;
using User.Domain;
using User.Messages;

namespace DespachoJuridicoDESIWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Autentication")]
    public class AutenticationController : ApiController
    {
        private readonly IUserApp _userApp;
        public AutenticationController(IUserApp userApp)
        {
            _userApp = userApp;
        }
        [HttpPost]
        [Route("GetUsuarioByCorreo")]
        public IHttpActionResult GetUsuarioByCorreo(UserMassagesRequest request)
        {
            UserMassagesResponse response = new UserMassagesResponse();
            response.UserObjs = _userApp.GetUsuarioByCorreo(request.email, out var operationResult);
            response.Result = operationResult;

            return Ok(response);
        }

        [HttpGet]
        [Route("GetUsuarioByProveedor")]
        public IHttpActionResult GetUsuarioByProveedor(string tipoAutenticacion, string usuarioId)
        {
            var result = _userApp.GetUsuarioByProveedor(tipoAutenticacion, usuarioId, out var operationResult);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUsuario")]
        public IHttpActionResult DeleteUsuario(long id)
        {
            var result = _userApp.DeleteUsuario(id, out var operationResult);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveOrUpdateUsuario")]
        public IHttpActionResult SaveOrUpdateUsuario(UserObj usuario)
        {
            var result = _userApp.SaveOrUpdateUsuario(usuario, out var operationResult);
            return Ok(result);
        }
    }
}
