using Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;
using User.Proxy;

namespace User.Application
{
    public class UserApp : IUserApp
    {
        private readonly IUserProxy _proxy;

        public UserApp(IUserProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<UserObj> GetUsuarioByCorreo(string correo, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetUsuarioByCorreo(correo);
                return UserMapp.MappUsuario(responseDT) ?? new List<UserObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener usuario por correo." }
            };
                return new List<UserObj>();
            }
        }

        public List<UserObj> GetUsuarioByProveedor(string tipoAutenticacion, string usuarioId, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetUsuarioByProveedor(tipoAutenticacion, usuarioId);
                return UserMapp.MappUsuario(responseDT) ?? new List<UserObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener usuario por proveedor." }
            };
                return new List<UserObj>();
            }
        }

        public DateTime DeleteUsuario(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteUsuario(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar usuario." }
            };
                return DateTime.MinValue;
            }
        }

        public UserObj SaveOrUpdateUsuario(UserObj usuario, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateUsuario(
                    usuario.Id,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.TipoAutenticacion,
                    usuario.PasswordHash,
                    usuario.ProveedorUserId,
                    usuario.Estatus,
                    usuario.CreatedBy,
                    usuario.CreatedDt,
                    usuario.UpdatedBy,
                    usuario.UpdatedDt
                );
                return UserMapp.MappUsuario(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar usuario." }
            };
                return null;
            }
        }
    }
}
