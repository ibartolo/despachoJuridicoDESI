using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Domain
{
    public class DashboardObj
    {
        // Sección 1: Tarjetas de resumen
        public SummaryCards Summary { get; set; }

        // Sección 2: Próximos eventos
        public List<UpcomingEvent> UpcomingEvents { get; set; }

        // Sección 3: Actividad reciente
        public List<RecentActivity> RecentActivities { get; set; }

        public DashboardObj()
        {
            Summary = new SummaryCards();
            UpcomingEvents = new List<UpcomingEvent>();
            RecentActivities = new List<RecentActivity>();
        }
    }

    public class SummaryCards
    {
        public int CasosActivos { get; set; }
        public int NuevosCasosSemana { get; set; }
        public int ExpedientesTotales { get; set; }
        public int ClientesActivos { get; set; }
        public int NuevosClientesMes { get; set; }
        public decimal IngresosTotales { get; set; }
    }

    public class UpcomingEvent
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string CasoNumero { get; set; }
        public string TipoEvento { get; set; }
    }

    public class RecentActivity
    {
        public string Actividad { get; set; }
        public string Referencia { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
    }
}
