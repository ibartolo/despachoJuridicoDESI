using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Proxy
{
    public interface IStatusCaseProxy
    {
        DataTable GetAllEstatusCaso();
        DataTable GetEstatusCasoById(long id);
        DateTime DeleteEstatusCaso(long id);
        DataTable SaveOrUpdateEstatusCaso(long id, string nombre, string descripcion, int orden,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
