using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Proxy
{
    public interface ICaseProxy
    {
        DataTable GetAllCaso();
        DataTable GetCasoById(long id);
        DataTable GetCasoByClienteId(long clienteId);
        DataTable SaveOrUpdateCaso(long id, long clienteId, long estatusCasoId, string numeroCaso,
            string descripcion, decimal montoInicial, bool estatus, string createdBy,
            DateTime? createdDt, string updatedBy, DateTime? updatedDt);
        DateTime DeleteCaso(long id);
    }
}
