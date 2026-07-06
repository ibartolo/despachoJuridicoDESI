using Case.Application;
using Case.Proxy;
using Common.Domain;
using Court.Proxy;
using Record.Domain;
using Record.Messages;
using Record.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Record.Application
{
    public class RecordApp : IRecordApp
    {
        private readonly IRecordProxy _proxy;

        public RecordApp(IRecordProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<RecordObj> GetAllRecords(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllRecords();
                return RecordMapp.MappRecord(responseDT) ?? new List<RecordObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los expedientes." }
            };
                return new List<RecordObj>();
            }
        }

        public List<RecordObj> GetRecordsByCaseId(long caseId, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetRecordByCaseId(caseId);
                return RecordMapp.MappRecord(responseDT) ?? new List<RecordObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener expedientes por caso." }
            };
                return new List<RecordObj>();
            }
        }

        public RecordObj GetRecordById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetRecordById(id);
                return RecordMapp.MappRecord(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener expediente por ID." }
            };
                return null;
            }
        }

        public DateTime DeleteRecord(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteRecord(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar expediente." }
            };
                return DateTime.MinValue;
            }
        }

        public RecordObj SaveOrUpdateRecord(RecordEntity record, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateRecord(
                    record.Id,
                    record.Case.Id,
                    record.Court.Id,
                    record.RecordNumber,
                    record.Comentarios,
                    record.Status,
                    record.CreatedBy,
                    record.CreatedDt,
                    record.UpdatedBy,
                    record.UpdatedDt
                );
                return RecordMapp.MappRecord(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar expediente." }
            };
                return null;
            }
        }
    }
}
