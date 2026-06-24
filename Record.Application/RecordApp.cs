using Case.Application;
using Case.Proxy;
using Common.Domain;
using Court.Proxy;
using Record.Domain;
using Record.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Record.Application
{
    public class RecordApp : IRecordApp
    {
        private readonly IRecordProxy _recordProxy;

        public RecordApp(IRecordProxy recordProxy)
        {
            _recordProxy = recordProxy ?? throw new ArgumentNullException(nameof(recordProxy));
        }

        public List<RecordObj> GetRecordsByCaseId(long caseId, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _recordProxy.GetRecordByCaseId(caseId);
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
                DataTable responseDT = _recordProxy.GetRecordByCaseId(id);
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
                return _recordProxy.DeleteRecord(id);
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

        public RecordObj SaveOrUpdateRecord(RecordObj record, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                long caseId = record.Case?.Id ?? 0;
                long courtId = record.Court?.Id ?? 0;

                DataTable responseDT = _recordProxy.SaveOrUpdateRecord(
                    record.Id,
                    caseId,
                    courtId,
                    record.RecordNumber,
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
