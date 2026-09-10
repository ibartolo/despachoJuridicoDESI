using Case.Domain;
using Court.Domain;
using Customer.Domain;
using Record.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Proxy
{

    public class RecordMapp
    {
        public static List<RecordObj> MappRecord(DataTable dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "No fue posible obtener valores desde el DataTable.");

            var list = new List<RecordObj>();

            if (dto.Rows.Count == 0)
                return list;

            foreach (DataRow row in dto.Rows)
            {
                #region Datos del Expediente
                long id = 0;
                if (dto.Columns.Contains("Id") && row["Id"] != DBNull.Value)
                {
                    try { id = Convert.ToInt64(row["Id"]); } catch { id = 0; }
                }

                string recordNumber = dto.Columns.Contains("NumeroExpediente") && row["NumeroExpediente"] != DBNull.Value
                    ? row["NumeroExpediente"].ToString() ?? string.Empty
                    : string.Empty;

                string comentarios = dto.Columns.Contains("Comentarios") && row["Comentarios"] != DBNull.Value
                    ? row["Comentarios"].ToString() ?? string.Empty
                    : string.Empty;

                bool status = false;
                if (dto.Columns.Contains("Estatus") && row["Estatus"] != DBNull.Value)
                {
                    try { status = Convert.ToBoolean(row["Estatus"]); } catch { status = false; }
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

                #region Datos del Caso
                CaseObj caseObj = null;
                if (dto.Columns.Contains("Caso_Id") && row["Caso_Id"] != DBNull.Value)
                {
                    long caseId = Convert.ToInt64(row["Caso_Id"]);
                    caseObj = CaseObj.Create(caseId);

                    string caseNumber = dto.Columns.Contains("Caso_NumeroCaso") && row["Caso_NumeroCaso"] != DBNull.Value
                        ? row["Caso_NumeroCaso"].ToString() ?? string.Empty
                        : string.Empty;
                    string caseDescription = dto.Columns.Contains("Caso_Descripcion") && row["Caso_Descripcion"] != DBNull.Value
                        ? row["Caso_Descripcion"].ToString() ?? string.Empty
                        : string.Empty;
                    decimal caseAmount = 0;
                    if (dto.Columns.Contains("Caso_MontoInicial") && row["Caso_MontoInicial"] != DBNull.Value)
                    {
                        try { caseAmount = Convert.ToDecimal(row["Caso_MontoInicial"]); } catch { caseAmount = 0; }
                    }
                    bool caseStatus = false;
                    if (dto.Columns.Contains("Caso_Estatus") && row["Caso_Estatus"] != DBNull.Value)
                    {
                        try { caseStatus = Convert.ToBoolean(row["Caso_Estatus"]); } catch { caseStatus = false; }
                    }

                    caseObj.SetInformacionCaso(caseNumber, caseDescription, caseAmount);
                    caseObj.SetEstatus(caseStatus);

                    if (dto.Columns.Contains("Caso_ClienteId") && row["Caso_ClienteId"] != DBNull.Value)
                    {
                        long clientId = Convert.ToInt64(row["Caso_ClienteId"]);
                        var client = ClientObj.Create(clientId);
                        caseObj.SetCliente(client);
                    }
                }
                #endregion

                #region Datos del Juzgado
                CourtObj courtObj = null;
                if (dto.Columns.Contains("Juzgado_Id") && row["Juzgado_Id"] != DBNull.Value)
                {
                    long courtId = Convert.ToInt64(row["Juzgado_Id"]);
                    courtObj = CourtObj.Create(courtId);

                    string courtName = dto.Columns.Contains("Juzgado_Nombre") && row["Juzgado_Nombre"] != DBNull.Value
                        ? row["Juzgado_Nombre"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtAddress = dto.Columns.Contains("Juzgado_Direccion") && row["Juzgado_Direccion"] != DBNull.Value
                        ? row["Juzgado_Direccion"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtColony = dto.Columns.Contains("Juzgado_Colonia") && row["Juzgado_Colonia"] != DBNull.Value
                        ? row["Juzgado_Colonia"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtCity = dto.Columns.Contains("Juzgado_Ciudad") && row["Juzgado_Ciudad"] != DBNull.Value
                        ? row["Juzgado_Ciudad"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtState = dto.Columns.Contains("Juzgado_Estado") && row["Juzgado_Estado"] != DBNull.Value
                        ? row["Juzgado_Estado"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtPostalCode = dto.Columns.Contains("Juzgado_CodigoPostal") && row["Juzgado_CodigoPostal"] != DBNull.Value
                        ? row["Juzgado_CodigoPostal"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtPhone = dto.Columns.Contains("Juzgado_Telefono") && row["Juzgado_Telefono"] != DBNull.Value
                        ? row["Juzgado_Telefono"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtSchedule = dto.Columns.Contains("Juzgado_Horario") && row["Juzgado_Horario"] != DBNull.Value
                        ? row["Juzgado_Horario"].ToString() ?? string.Empty
                        : string.Empty;
                    string courtObservations = dto.Columns.Contains("Juzgado_Observaciones") && row["Juzgado_Observaciones"] != DBNull.Value
                        ? row["Juzgado_Observaciones"].ToString() ?? string.Empty
                        : string.Empty;
                    bool courtStatus = false;
                    if (dto.Columns.Contains("Juzgado_Estatus") && row["Juzgado_Estatus"] != DBNull.Value)
                    {
                        try { courtStatus = Convert.ToBoolean(row["Juzgado_Estatus"]); } catch { courtStatus = false; }
                    }

                    courtObj.SetInformacionBasica(courtName, courtAddress, courtColony, courtCity, courtState, courtPostalCode);
                    courtObj.SetContacto(courtPhone, courtSchedule);
                    courtObj.SetObservaciones(courtObservations);
                    courtObj.SetEstatus(courtStatus);
                }
                #endregion

                #region Creación del objeto Record
                var item = RecordObj.Create(id);
                if (item != null)
                {
                    if (caseObj != null)
                        item.SetCase(caseObj);

                    if (courtObj != null)
                        item.SetCourt(courtObj);

                    item.SetRecordNumber(recordNumber);
                    item.SetComentarios(comentarios);
                    item.SetStatus(status);

                    if (createdDt.HasValue)
                    {
                        item.SetAuditCreation(createdBy, createdDt.Value);
                    }
                    if (updatedDt.HasValue)
                    {
                        item.SetAuditUpdate(updatedBy, updatedDt.Value);
                    }

                    list.Add(item);
                }
                #endregion
            }

            return list;
        }
    }

}
