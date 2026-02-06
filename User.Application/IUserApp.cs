using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;

namespace User.Application
{
    public interface IUserApp
    {
        List<UserObj> GetUsuarioByCorreo(string correo, out OperationResult result);
        List<UserObj> GetUsuarioByProveedor(string tipoAutenticacion, string usuarioId, out OperationResult result);
        DateTime DeleteUsuario(long id, out OperationResult result);
        UserObj SaveOrUpdateUsuario(UserObj usuario, out OperationResult result);
        UserObj AutenticacionParaToken(string correo, string pass);
    }
}
