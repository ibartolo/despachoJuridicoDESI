using Common.Domain;
using Record.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Messages
{
    public class RecordMessagesResponse
    {
        public List<RecordObj> RecordObjs { get; set; }
        public OperationResult Result { get; set; }
    }

    public class GetRecordByIdRequest
    {
        public long Id { get; set; }
    }

    public class GetRecordByCaseIdRequest
    {
        public long CaseId { get; set; }
    }

    public class DeleteRecordRequest
    {
        public long Id { get; set; }
    }

    public class SaveOrUpdateRecordRequest
    {
        public RecordObj Record { get; set; }
    }
}
