using Case.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Proxy
{
    public class CaseMapp
    {
        public static List<CaseObj> MappCaso(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<CaseObj>();

            if (dto.Rows.Count == 0)
                return list;

            foreach (DataRow row in dto.Rows)
            {
                long id = 0;
                if (dto.Columns.Contains("Id") && row["Id"] != DBNull.Value)
                {
                    try { id = Convert.ToInt64(row["Id"]); } catch { id = 0; }
                }

                long clienteId = 0;
                if (dto.Columns.Contains("ClienteId") && row["ClienteId"] != DBNull.Value)
                {
                    try { clienteId = Convert.ToInt64(row["ClienteId"]); } catch { clienteId = 0; }
                }

                long estatusCasoId = 0;
                if (dto.Columns.Contains("EstatusCasoId") && row["EstatusCasoId"] != DBNull.Value)
                {
                    try { estatusCasoId = Convert.ToInt64(row["EstatusCasoId"]); } catch { estatusCasoId = 0; }
                }

                string numeroCaso = dto.Columns.Contains("NumeroCaso") && row["NumeroCaso"] != DBNull.Value
                    ? row["NumeroCaso"].ToString() ?? string.Empty
                    : string.Empty;

                string descripcion = dto.Columns.Contains("Descripcion") && row["Descripcion"] != DBNull.Value
                    ? row["Descripcion"].ToString() ?? string.Empty
                    : string.Empty;

                decimal montoInicial = 0;
                if (dto.Columns.Contains("MontoInicial") && row["MontoInicial"] != DBNull.Value)
                {
                    try { montoInicial = Convert.ToDecimal(row["MontoInicial"]); } catch { montoInicial = 0; }
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

                var item = CaseObj.Create(id);
                if (item != null)
                {
                    item.SetClienteId(clienteId);

                    // Temporal: solo asignamos el ID, luego se poblará el objeto StatusCase
                    var statusCase = StatusCaseObj.Create(estatusCasoId);
                    item.SetEstatusCaso(statusCase);

                    item.SetInformacionCaso(numeroCaso, descripcion, montoInicial);
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
