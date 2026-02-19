using Case.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Messages
{
    public class CaseMessages
    {
        public class StatusCaseMessagesResponse
        {
            public List<StatusCaseObj> StatusCaseObjs { get; set; }
            public OperationResult Result { get; set; }
        }

        public class GetStatusCaseByIdRequest
        {
            public long Id { get; set; }
        }

        public class DeleteStatusCaseRequest
        {
            public long Id { get; set; }
        }

        public class SaveOrUpdateStatusCaseRequest
        {
            public StatusCaseObj StatusCase { get; set; }
        }
    }
}
