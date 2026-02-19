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
    public class StatusCaseProxy : DbWrapper, IStatusCaseProxy
    {
        public DataTable GetAllEstatusCaso()
        {
            return GetObject("GetAllEstatusCaso", CommandType.StoredProcedure);
        }

        public DataTable GetEstatusCasoById(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            return GetObject("GetEstatusCasoById", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteEstatusCaso(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteEstatusCaso", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }

        public DataTable SaveOrUpdateEstatusCaso(long id, string nombre, string descripcion, int orden,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@Descripcion", (object)descripcion ?? DBNull.Value),
            new SqlParameter("@Orden", orden),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateEstatusCaso", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
