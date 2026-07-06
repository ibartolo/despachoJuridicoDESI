using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Proxy
{
    public interface IAgendaProxy
    {
        DataTable GetAllAgenda();
        DataTable GetAgendaById(long id);
        DataTable GetAgendaByCasoId(long casoId);
        DataTable GetAgendaByFecha(DateTime fechaInicio, DateTime fechaFin);
        DateTime DeleteAgenda(long id);
        DataTable SaveOrUpdateAgenda(long id, long casoId, long tipoEventoId, string titulo, string descripcion,
            DateTime fechaInicio, DateTime? fechaFin, string googleEventId, bool? sincronizadoGoogle,
            DateTime? fechaSincronizacion, bool estatus, string createdBy, DateTime? createdDt,
            string updatedBy, DateTime? updatedDt);
    }
}
