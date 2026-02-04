using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;

namespace User.Proxy
{
    public class UserMapp
    {
        public static List<UserObj> MappUsuario(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<UserObj>();

            if (dto.Rows.Count == 0)
                return list;

            foreach (DataRow row in dto.Rows)
            {
                long id = 0;
                if (dto.Columns.Contains("Id") && row["Id"] != DBNull.Value)
                {
                    try { id = Convert.ToInt64(row["Id"]); } catch { id = 0; }
                }

                string nombre = dto.Columns.Contains("Nombre") && row["Nombre"] != DBNull.Value
                    ? row["Nombre"].ToString() ?? string.Empty
                    : string.Empty;
                string correo = dto.Columns.Contains("Correo") && row["Correo"] != DBNull.Value
                    ? row["Correo"].ToString() ?? string.Empty
                    : string.Empty;
                string tipoAutenticacion = dto.Columns.Contains("TipoAutenticacion") && row["TipoAutenticacion"] != DBNull.Value
                    ? row["TipoAutenticacion"].ToString() ?? string.Empty
                    : string.Empty;
                string passwordHash = dto.Columns.Contains("PasswordHash") && row["PasswordHash"] != DBNull.Value
                    ? row["PasswordHash"].ToString() ?? string.Empty
                    : string.Empty;
                string proveedorUserId = dto.Columns.Contains("ProveedorUserId") && row["ProveedorUserId"] != DBNull.Value
                    ? row["ProveedorUserId"].ToString() ?? string.Empty
                    : string.Empty;

                bool estatus = false;
                if (dto.Columns.Contains("Estatus") && row["Estatus"] != DBNull.Value)
                {
                    try { estatus = Convert.ToBoolean(row["Estatus"]); } catch { estatus = false; }
                }

                string createdBy = dto.Columns.Contains("CreatedBy") && row["CreatedBy"] != DBNull.Value
                    ? row["CreatedBy"].ToString() ?? string.Empty
                    : string.Empty;
                DateTime? createdDt = null;
                if (dto.Columns.Contains("CreatedDt") && row["CreatedDt"] != DBNull.Value)
                {
                    try { createdDt = Convert.ToDateTime(row["CreatedDt"]); } catch { createdDt = null; }
                }

                string updatedBy = dto.Columns.Contains("UpdatedBy") && row["UpdatedBy"] != DBNull.Value
                    ? row["UpdatedBy"].ToString() ?? string.Empty
                    : string.Empty;
                DateTime? updatedDt = null;
                if (dto.Columns.Contains("UpdatedDt") && row["UpdatedDt"] != DBNull.Value)
                {
                    try { updatedDt = Convert.ToDateTime(row["UpdatedDt"]); } catch { updatedDt = null; }
                }

                var item = UserObj.Create(id);
                if (item != null)
                {
                    item.SetInformacionBasica(nombre, correo);
                    item.SetAutenticacion(tipoAutenticacion, passwordHash, proveedorUserId);
                    item.SetEstatus(estatus);

                    if (createdDt.HasValue)
                    {
                        item.SetAuditoriaCreacion(createdBy, createdDt.Value);
                    }
                    if (updatedDt.HasValue)
                    {
                        item.SetAuditoriaActualizacion(updatedBy, updatedDt.Value);
                    }

                    list.Add(item);
                }
            }

            return list;
        }
    }
}
