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
        DataTable GetRecordByCaseId(long caseId);
        DataTable SaveOrUpdateRecord(long id, long caseId, long courtId, string recordNumber,
            bool status, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
        DateTime DeleteRecord(long id);
    }
}
