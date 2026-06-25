using Common.Domain;
using Record.Domain;
using Record.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Application
{
    public interface IRecordApp
    {
        List<RecordObj> GetAllRecords(out OperationResult result);
        List<RecordObj> GetRecordsByCaseId(long caseId, out OperationResult result);
        RecordObj GetRecordById(long id, out OperationResult result);
        DateTime DeleteRecord(long id, out OperationResult result);
        RecordObj SaveOrUpdateRecord(RecordEntity record, out OperationResult result);
    }
}
