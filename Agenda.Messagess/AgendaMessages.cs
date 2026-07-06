using Agenda.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Messagess
{
    public class AgendaMessagesResponse
    {
        public List<AgendaEntity> AgendaItems { get; set; }
        public OperationResult Result { get; set; }
    }

    public class GetAgendaByIdRequest
    {
        public long Id { get; set; }
    }

    public class GetAgendaByCasoIdRequest
    {
        public long CasoId { get; set; }
    }

    public class GetAgendaByFechaRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class DeleteAgendaRequest
    {
        public long Id { get; set; }
    }

    public class SaveOrUpdateAgendaRequest
    {
        public AgendaEntity Agenda { get; set; }
    }
}
