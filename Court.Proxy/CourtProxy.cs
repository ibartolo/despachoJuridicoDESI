using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Proxy
{
    public class CourtProxy : DbWrapper, ICourtProxy
    {
        public DataTable GetAllCourts()
        {
            return GetObject("GetAllJuzgado", CommandType.StoredProcedure);
        }

        public DataTable GetCourtById(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            return GetObject("GetJuzgadoById", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteCourt(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteJuzgado", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }

        public DataTable SaveOrUpdateCourt(long id, string nombre, string direccion, string colonia, string ciudad,
            string estado, string codigoPostal, string telefono, string horario, string observaciones,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@Direccion", direccion),
            new SqlParameter("@Colonia", (object)colonia ?? DBNull.Value),
            new SqlParameter("@Ciudad", (object)ciudad ?? DBNull.Value),
            new SqlParameter("@Estado", (object)estado ?? DBNull.Value),
            new SqlParameter("@CodigoPostal", (object)codigoPostal ?? DBNull.Value),
            new SqlParameter("@Telefono", (object)telefono ?? DBNull.Value),
            new SqlParameter("@Horario", (object)horario ?? DBNull.Value),
            new SqlParameter("@Observaciones", (object)observaciones ?? DBNull.Value),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateJuzgado", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
