using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Proxy
{
    public class AgendaProxy : DbWrapper, IAgendaProxy
    {
        public DataTable GetAllAgenda()
        {
            return GetObject("GetAllAgenda", CommandType.StoredProcedure);
        }

        public DataTable GetAgendaById(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            return GetObject("GetAgendaById", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable GetAgendaByCasoId(long casoId)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@CasoId", casoId)
            };
            return GetObject("GetAgendaByCasoId", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable GetAgendaByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@FechaInicio", fechaInicio),
            new SqlParameter("@FechaFin", fechaFin)
            };
            return GetObject("GetAgendaByFecha", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteAgenda(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteAgenda", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }

        public DataTable SaveOrUpdateAgenda(long id, long casoId, long tipoEventoId, string titulo, string descripcion,
            DateTime fechaInicio, DateTime? fechaFin, string googleEventId, bool? sincronizadoGoogle,
            DateTime? fechaSincronizacion, bool estatus, string createdBy, DateTime? createdDt,
            string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@CasoId", casoId),
            new SqlParameter("@TipoEventoId", tipoEventoId),
            new SqlParameter("@Titulo", titulo),
            new SqlParameter("@Descripcion", (object)descripcion ?? DBNull.Value),
            new SqlParameter("@FechaInicio", fechaInicio),
            new SqlParameter("@FechaFin", (object)fechaFin ?? DBNull.Value),
            new SqlParameter("@GoogleEventId", (object)googleEventId ?? DBNull.Value),
            new SqlParameter("@SincronizadoGoogle", (object)sincronizadoGoogle ?? DBNull.Value),
            new SqlParameter("@FechaSincronizacion", (object)fechaSincronizacion ?? DBNull.Value),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateAgenda", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
