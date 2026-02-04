using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Proxy
{
    public class UserProxy : DbWrapper, IUserProxy
    {
        public DateTime DeleteUsuario(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteUsuario", CommandType.StoredProcedure, sqlParameters);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("DeletedDt"))
            {
                return Convert.ToDateTime(dt.Rows[0]["DeletedDt"]);
            }
            return DateTime.MinValue;
        }

        public DataTable GetUsuarioByCorreo(string correo)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Correo", correo)
            };
            return GetObject("GetUsuarioByCorreo", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable GetUsuarioByProveedor(string tipoAutenticacion, string usuarioId)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@TipoAutenticacion", tipoAutenticacion),
            new SqlParameter("@ProveedorUserId", usuarioId)
            };
            return GetObject("GetUsuarioByProveedor", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable SaveOrUpdateUsuario(long id, string nombre, string correo, string tipoAutenticacion, string passwordHash,
            string proveedorUserId, bool estatus, string creayedBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@Correo", correo),
            new SqlParameter("@TipoAutenticacion", tipoAutenticacion),
            new SqlParameter("@PasswordHash", passwordHash),
            new SqlParameter("@ProveedorUserId", proveedorUserId),
            new SqlParameter("@Estatus", estatus),
            new SqlParameter("@CreatedBy", creayedBy),
            new SqlParameter("@CreatedDt", createdDt),
            new SqlParameter("@UpdatedBy", updatedBy),
            new SqlParameter("@UpdatedDt", updatedDt)
            };
            return GetObject("SaveOrUpdateUsuario", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
