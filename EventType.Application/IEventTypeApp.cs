using Common.Domain;
using EventType.Domain;
using EventType.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventType.Application
{
    public interface IEventTypeApp
    {
        List<EventTypeObj> GetAllEventTypes(out OperationResult result);
        EventTypeObj GetEventTypeById(long id, out OperationResult result);
        DateTime DeleteEventType(long id, out OperationResult result);
        EventTypeObj SaveOrUpdateEventType(EventTypeEntity eventType, out OperationResult result);
    }
}
