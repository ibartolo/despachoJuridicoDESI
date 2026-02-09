using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Proxy
{
    public class ClientProxy : DbWrapper, IClientProxy
    {
        public DataTable GetAllCliente()
        {
            return GetObject("GetAllCliente", CommandType.StoredProcedure);
        }

        public DataTable GetClienteById(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            return GetObject("GetClienteById", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteCliente(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteCliente", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }

        public DataTable SaveOrUpdateCliente(long id, string nombre, string telefono, string correo,
            bool estatus, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@Telefono", (object)telefono ?? DBNull.Value),
            new SqlParameter("@Correo", (object)correo ?? DBNull.Value),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateCliente", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
