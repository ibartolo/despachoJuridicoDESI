using DespachoJuridicoDESIMVC.Models.Common;
using DespachoJuridicoDESIMVC.Models.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{

    public class SaveOrUpdateEventTypeRequest
    {
        public EventTypeObj EventType { get; set; }
    }

    public class EventTypeMassagesResponse
    {
        public List<EventTypeObj> EventTypeObjs { get; set; } = new List<EventTypeObj>();
        public OperationResult Result { get; set; } = new OperationResult();
    }

    public class EventTypeDeleteResponse
    {
        public DateTime DeletedDt { get; set; }
        public OperationResult Result { get; set; } = new OperationResult();
    }
}