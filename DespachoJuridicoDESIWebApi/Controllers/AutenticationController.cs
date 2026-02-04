using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using User.Application;
using User.Domain;

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
        [HttpGet]
        [Route("GetUsuarioByCorreo")]
        public IHttpActionResult GetUsuarioByCorreo(string correo)
        {
            var result = _userApp.GetUsuarioByCorreo(correo, out var operationResult);
            return Ok(result);
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
