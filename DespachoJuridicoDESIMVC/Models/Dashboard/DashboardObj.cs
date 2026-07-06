using DespachoJuridicoDESIMVC.Models.Common;
using System;
using System.Collections.Generic;

namespace DespachoJuridicoDESIMVC.Models.Dashboard
{
    public class DashboardMassagesResponse
    {
        public DashboardObj Dashboard { get; set; } = new DashboardObj();
        public OperationResult Result { get; set; } = new OperationResult();
    }

    public class DashboardObj
    {
        // Sección 1: Tarjetas de resumen
        public SummaryCards Summary { get; set; } = new SummaryCards();

        // Sección 2: Próximos eventos
        public List<UpcomingEvent> UpcomingEvents { get; set; } = new List<UpcomingEvent>();

        // Sección 3: Actividad reciente
        public List<RecentActivity> RecentActivities { get; set; } = new List<RecentActivity>();
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
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string CasoNumero { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty;
    }

    public class RecentActivity
    {
        public string Actividad { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; } = string.Empty;
    }
}