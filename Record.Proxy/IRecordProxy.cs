using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Proxy
{
    public interface IRecordProxy
    {
        DataTable GetAllRecords();
        DataTable GetRecordByCaseId(long caseId);
        DataTable SaveOrUpdateRecord(long id, long caseId, long courtId, string recordNumber,
            string comentarios, bool status, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
        DateTime DeleteRecord(long id);
        DataTable GetRecordById(long id);
    }
}
