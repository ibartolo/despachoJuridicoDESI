using Agenda.Domain;
using Case.Domain;
using EventType.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Proxy
{
    public class AgendaMapp
    {
        public static List<AgendaObj> MappAgenda(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<AgendaObj>();

            if (dto.Rows.Count == 0)
                return list;

            foreach (DataRow row in dto.Rows)
            {
                #region Datos de Agenda
                long id = 0;
                if (dto.Columns.Contains("Id") && row["Id"] != DBNull.Value)
                {
                    try { id = Convert.ToInt64(row["Id"]); } catch { id = 0; }
                }

                long casoId = 0;
                if (dto.Columns.Contains("CasoId") && row["CasoId"] != DBNull.Value)
                {
                    try { casoId = Convert.ToInt64(row["CasoId"]); } catch { casoId = 0; }
                }

                long tipoEventoId = 0;
                if (dto.Columns.Contains("TipoEventoId") && row["TipoEventoId"] != DBNull.Value)
                {
                    try { tipoEventoId = Convert.ToInt64(row["TipoEventoId"]); } catch { tipoEventoId = 0; }
                }

                string titulo = dto.Columns.Contains("Titulo") && row["Titulo"] != DBNull.Value
                    ? row["Titulo"].ToString() ?? string.Empty
                    : string.Empty;
                string descripcion = dto.Columns.Contains("Descripcion") && row["Descripcion"] != DBNull.Value
                    ? row["Descripcion"].ToString() ?? string.Empty
                    : string.Empty;

                DateTime fechaInicio = DateTime.MinValue;
                if (dto.Columns.Contains("FechaInicio") && row["FechaInicio"] != DBNull.Value)
                {
                    try { fechaInicio = Convert.ToDateTime(row["FechaInicio"]); } catch { fechaInicio = DateTime.MinValue; }
                }

                DateTime? fechaFin = null;
                if (dto.Columns.Contains("FechaFin") && row["FechaFin"] != DBNull.Value)
                {
                    try { fechaFin = Convert.ToDateTime(row["FechaFin"]); } catch { fechaFin = null; }
                }

                string googleEventId = dto.Columns.Contains("GoogleEventId") && row["GoogleEventId"] != DBNull.Value
                    ? row["GoogleEventId"].ToString() ?? string.Empty
                    : string.Empty;

                bool? sincronizadoGoogle = null;
                if (dto.Columns.Contains("SincronizadoGoogle") && row["SincronizadoGoogle"] != DBNull.Value)
                {
                    try { sincronizadoGoogle = Convert.ToBoolean(row["SincronizadoGoogle"]); } catch { sincronizadoGoogle = null; }
                }

                DateTime? fechaSincronizacion = null;
                if (dto.Columns.Contains("FechaSincronizacion") && row["FechaSincronizacion"] != DBNull.Value)
                {
                    try { fechaSincronizacion = Convert.ToDateTime(row["FechaSincronizacion"]); } catch { fechaSincronizacion = null; }
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
                #endregion

                #region Crear objetos relacionados
                CaseObj casoObj = CaseObj.Create(casoId);
                EventTypeObj eventTypeObj = EventTypeObj.Create(tipoEventoId);
                #endregion

                var item = AgendaObj.Create(id);
                if (item != null)
                {
                    item.SetCaso(casoObj);
                    item.SetTipoEvento(eventTypeObj);
                    item.SetInformacion(titulo, descripcion, fechaInicio, fechaFin);
                    item.SetGoogleInfo(googleEventId, sincronizadoGoogle, fechaSincronizacion);
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
