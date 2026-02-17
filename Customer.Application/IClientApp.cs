using Common.Domain;
using Customer.Domain;
using Customer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application
{
    public interface IClientApp
    {
        List<ClientObj> GetAllClientes(out OperationResult result);
        ClientObj GetClienteById(long id, out OperationResult result);
        DateTime DeleteCliente(long id, out OperationResult result);
        ClientObj SaveOrUpdateCliente(ClientEntity cliente, out OperationResult result);
    }
}
