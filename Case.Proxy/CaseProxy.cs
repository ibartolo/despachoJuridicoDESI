using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Proxy
{
    public class CaseProxy : DbWrapper, ICaseProxy
    {
        public DataTable GetAllCaso()
        {
            return GetObject("GetAllCaso", CommandType.StoredProcedure);
        }

        public DataTable GetCasoById(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            return GetObject("GetCasoById", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable GetCasoByClienteId(long clienteId)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@ClienteId", clienteId)
            };
            return GetObject("GetCasoByClienteId", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable SaveOrUpdateCaso(long id, long clienteId, long estatusCasoId, string numeroCaso,
            string descripcion, decimal montoInicial, bool estatus, string createdBy,
            DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@ClienteId", clienteId),
            new SqlParameter("@EstatusCasoId", estatusCasoId),
            new SqlParameter("@NumeroCaso", numeroCaso),
            new SqlParameter("@Descripcion", (object)descripcion ?? DBNull.Value),
            new SqlParameter("@MontoInicial", montoInicial),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateCaso", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteCaso(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteCaso", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }
    }
}
