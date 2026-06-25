using DespachoJuridicoDESIMVC.Models.Common;
using DespachoJuridicoDESIMVC.Models.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class SaveOrUpdateRecordRequest
    {
        public RecordObj Record { get; set; }
    }

    public class RecordMassagesResponse
    {
        public List<RecordObj> RecordObjs { get; set; } = new List<RecordObj>();
        public OperationResult Result { get; set; } = new OperationResult();
    }

    public class RecordDeleteResponse
    {
        public DateTime DeletedDt { get; set; }
        public OperationResult Result { get; set; } = new OperationResult();
    }
}