using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Proxy
{
    public class RecordProxy : DbWrapper, IRecordProxy
    {
        public DataTable GetRecordByCaseId(long caseId)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@CasoId", caseId)
            };
            return GetObject("GetExpedienteByCasoId", CommandType.StoredProcedure, sqlParameters);
        }

        public DataTable SaveOrUpdateRecord(long id, long caseId, long courtId, string recordNumber,
            bool status, string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id),
            new SqlParameter("@CasoId", caseId),
            new SqlParameter("@JuzgadoId", courtId),
            new SqlParameter("@NumeroExpediente", recordNumber),
            new SqlParameter("@Estatus", status),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@CreatedDt", (object)createdDt ?? DBNull.Value),
            new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value),
            new SqlParameter("@UpdatedDt", (object)updatedDt ?? DBNull.Value)
            };
            return GetObject("SaveOrUpdateExpediente", CommandType.StoredProcedure, sqlParameters);
        }

        public DateTime DeleteRecord(long id)
        {
            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };
            DataTable dt = GetObject("DeleteExpediente", CommandType.StoredProcedure, sqlParameters);
            return DateTime.Now;
        }
    }
}
