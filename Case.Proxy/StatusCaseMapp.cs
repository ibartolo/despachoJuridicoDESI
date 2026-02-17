using Case.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Proxy
{
    public class StatusCaseMapp
    {
        public static List<StatusCaseObj> MappEstatusCaso(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<StatusCaseObj>();

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
                string descripcion = dto.Columns.Contains("Descripcion") && row["Descripcion"] != DBNull.Value
                    ? row["Descripcion"].ToString() ?? string.Empty
                    : string.Empty;

                int orden = 0;
                if (dto.Columns.Contains("Orden") && row["Orden"] != DBNull.Value)
                {
                    try { orden = Convert.ToInt32(row["Orden"]); } catch { orden = 0; }
                }

                bool generaEvento = false;
                if (dto.Columns.Contains("GeneraEvento") && row["GeneraEvento"] != DBNull.Value)
                {
                    try { generaEvento = Convert.ToBoolean(row["GeneraEvento"]); } catch { generaEvento = false; }
                }

                bool generaNotificacion = false;
                if (dto.Columns.Contains("GeneraNotificacion") && row["GeneraNotificacion"] != DBNull.Value)
                {
                    try { generaNotificacion = Convert.ToBoolean(row["GeneraNotificacion"]); } catch { generaNotificacion = false; }
                }

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

                var item = StatusCaseObj.Create(id);
                if (item != null)
                {
                    item.SetInformacionBasica(nombre, descripcion, orden);
                    item.SetOpciones(generaEvento, generaNotificacion);
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
