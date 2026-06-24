using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Proxy
{
    public interface ICourtProxy
    {
        DataTable GetAllCourts();
        DataTable GetCourtById(long id);
        DateTime DeleteCourt(long id);
        DataTable SaveOrUpdateCourt(long id, string nombre, string direccion, string colonia, string ciudad,
            string estado, string codigoPostal, string telefono, string horario, string observaciones,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
