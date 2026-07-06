using Common.Domain;
using Court.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Messages
{
    public class CourtMessages
    {
        public class CourtMessagesResponse
        {
            public CourtMessagesResponse() 
            {
                CourtObjs = new List<CourtObj>();
                Result = new OperationResult();
            }
            public List<CourtObj> CourtObjs { get; set; }
            public OperationResult Result { get; set; }
        }

        public class GetCourtByIdRequest
        {
            public long Id { get; set; }
        }

        public class DeleteCourtRequest
        {
            public long Id { get; set; }
        }

        public class SaveOrUpdateCourtRequest
        {
            public SaveOrUpdateCourtRequest()
            { 
                Court = new CourtEntity();
            }
            public CourtEntity Court { get; set; }
        }
    }
}
