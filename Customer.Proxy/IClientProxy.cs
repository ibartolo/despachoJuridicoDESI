using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Proxy
{
    public interface IClientProxy
    {
        DataTable GetAllCliente();
        DataTable GetClienteById(long id);
        DateTime DeleteCliente(long id);
        DataTable SaveOrUpdateCliente(long id, string nombre, string telefono, string correo,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
