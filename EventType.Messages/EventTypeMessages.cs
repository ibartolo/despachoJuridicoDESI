using Common.Domain;
using EventType.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventType.Messages
{
    public class EventTypeMessagesResponse
    {
        public List<EventTypeObj> EventTypeObjs { get; set; }
        public OperationResult Result { get; set; }
    }

    public class GetEventTypeByIdRequest
    {
        public long Id { get; set; }
    }

    public class DeleteEventTypeRequest
    {
        public long Id { get; set; }
    }

    public class SaveOrUpdateEventTypeRequest
    {
        public EventTypeEntity EventType { get; set; }
    }

    public class EventTypeEntity
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }
    }    
    
}
