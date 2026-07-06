using Dashboard.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Proxy
{
    public class DashboardMapp
    {
        public static DashboardObj MappDashboard(DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 3)
                throw new ArgumentNullException(nameof(ds), "No fue posible obtener valores desde el DataSet.");

            var dashboard = new DashboardObj();

            // Sección 1: Tarjetas de resumen (Tabla 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                dashboard.Summary = new SummaryCards
                {
                    CasosActivos = Convert.ToInt32(row["CasosActivos"]),
                    NuevosCasosSemana = Convert.ToInt32(row["NuevosCasosSemana"]),
                    ExpedientesTotales = Convert.ToInt32(row["ExpedientesTotales"]),
                    ClientesActivos = Convert.ToInt32(row["ClientesActivos"]),
                    NuevosClientesMes = Convert.ToInt32(row["NuevosClientesMes"]),
                    IngresosTotales = Convert.ToDecimal(row["IngresosTotales"])
                };
            }

            // Sección 2: Próximos eventos (Tabla 1)
            dashboard.UpcomingEvents = new List<UpcomingEvent>();
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                dashboard.UpcomingEvents.Add(new UpcomingEvent
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Titulo = row["Titulo"]?.ToString() ?? string.Empty,
                    Descripcion = row["Descripcion"]?.ToString() ?? string.Empty,
                    FechaInicio = Convert.ToDateTime(row["FechaInicio"]),
                    FechaFin = row["FechaFin"] != DBNull.Value ? Convert.ToDateTime(row["FechaFin"]) : (DateTime?)null,
                    CasoNumero = row["CasoNumero"]?.ToString() ?? string.Empty,
                    TipoEvento = row["TipoEvento"]?.ToString() ?? string.Empty
                });
            }

            // Sección 3: Actividad reciente (Tabla 2)
            dashboard.RecentActivities = new List<RecentActivity>();
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                dashboard.RecentActivities.Add(new RecentActivity
                {
                    Actividad = row["Actividad"]?.ToString() ?? string.Empty,
                    Referencia = row["Referencia"]?.ToString() ?? string.Empty,
                    Usuario = row["Usuario"]?.ToString() ?? string.Empty,
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Detalle = row["Detalle"]?.ToString() ?? string.Empty
                });
            }

            return dashboard;
        }
    }
}
