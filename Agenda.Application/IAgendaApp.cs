using Agenda.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application
{
    public interface IAgendaApp
    {
        List<AgendaEntity> GetAllAgenda(out OperationResult result);
        AgendaEntity GetAgendaById(long id, out OperationResult result);
        List<AgendaEntity> GetAgendaByCasoId(long casoId, out OperationResult result);
        List<AgendaEntity> GetAgendaByFecha(DateTime fechaInicio, DateTime fechaFin, out OperationResult result);
        DateTime DeleteAgenda(long id, out OperationResult result);
        AgendaEntity SaveOrUpdateAgenda(AgendaEntity agenda, out OperationResult result);
    }
}
