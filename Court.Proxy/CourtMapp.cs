using Court.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Proxy
{
    public class CourtMapp
    {
        public static List<CourtObj> MappCourt(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<CourtObj>();

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
                string direccion = dto.Columns.Contains("Direccion") && row["Direccion"] != DBNull.Value
                    ? row["Direccion"].ToString() ?? string.Empty
                    : string.Empty;
                string colonia = dto.Columns.Contains("Colonia") && row["Colonia"] != DBNull.Value
                    ? row["Colonia"].ToString() ?? string.Empty
                    : string.Empty;
                string ciudad = dto.Columns.Contains("Ciudad") && row["Ciudad"] != DBNull.Value
                    ? row["Ciudad"].ToString() ?? string.Empty
                    : string.Empty;
                string estado = dto.Columns.Contains("Estado") && row["Estado"] != DBNull.Value
                    ? row["Estado"].ToString() ?? string.Empty
                    : string.Empty;
                string codigoPostal = dto.Columns.Contains("CodigoPostal") && row["CodigoPostal"] != DBNull.Value
                    ? row["CodigoPostal"].ToString() ?? string.Empty
                    : string.Empty;
                string telefono = dto.Columns.Contains("Telefono") && row["Telefono"] != DBNull.Value
                    ? row["Telefono"].ToString() ?? string.Empty
                    : string.Empty;
                string horario = dto.Columns.Contains("Horario") && row["Horario"] != DBNull.Value
                    ? row["Horario"].ToString() ?? string.Empty
                    : string.Empty;
                string observaciones = dto.Columns.Contains("Observaciones") && row["Observaciones"] != DBNull.Value
                    ? row["Observaciones"].ToString() ?? string.Empty
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

                var item = CourtObj.Create(id);
                if (item != null)
                {
                    item.SetInformacionBasica(nombre, direccion, colonia, ciudad, estado, codigoPostal);
                    item.SetContacto(telefono, horario);
                    item.SetObservaciones(observaciones);
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
