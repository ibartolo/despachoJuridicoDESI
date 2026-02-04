using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Proxy
{
    public interface IUserProxy
    {
        DateTime DeleteUsuario(long id);
        DataTable GetUsuarioByCorreo(string correo);
        DataTable GetUsuarioByProveedor(string tipoAutenticacion, string usuarioId);
        DataTable SaveOrUpdateUsuario(long id, string nombre, string correo, string tipoAutenticacion, string passwordHash,
            string proveedorUserId, bool estatus, string creayedBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
