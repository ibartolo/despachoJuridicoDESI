using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Domain
{
    public class AgendaEntity
    {
        public long Id { get; set; }
        public long CasoId { get; set; }
        public long TipoEventoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string GoogleEventId { get; set; }
        public bool? SincronizadoGoogle { get; set; }
        public DateTime? FechaSincronizacion { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }
        public string NombreCaso { get; set; }
        public string NombreTipoEvento { get; set; }
    }
}
