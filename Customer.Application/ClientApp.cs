using Common.Domain;
using Customer.Domain;
using Customer.Messages;
using Customer.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application
{
    public class ClientApp : IClientApp
    {
        private readonly IClientProxy _proxy;

        public ClientApp(IClientProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<ClientObj> GetAllClientes(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllCliente();
                return ClientMapp.MappCliente(responseDT) ?? new List<ClientObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los clientes." }
            };
                return new List<ClientObj>();
            }
        }

        public ClientObj GetClienteById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetClienteById(id);
                return ClientMapp.MappCliente(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener cliente por ID." }
            };
                return null;
            }
        }

        public DateTime DeleteCliente(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteCliente(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar cliente." }
            };
                return DateTime.MinValue;
            }
        }

        public ClientObj SaveOrUpdateCliente(ClientEntity cliente, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateCliente(
                    cliente.Id,
                    cliente.Nombre,
                    cliente.Telefono,
                    cliente.Correo,
                    cliente.Estatus,
                    cliente.CreatedBy,
                    cliente.CreatedDt,
                    cliente.UpdatedBy,
                    cliente.UpdatedDt
                );
                return ClientMapp.MappCliente(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar cliente." }
            };
                return null;
            }
        }
    }
}
