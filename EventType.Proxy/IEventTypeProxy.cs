using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventType.Proxy
{
    public interface IEventTypeProxy
    {
        DataTable GetAllEventTypes();
        DataTable GetEventTypeById(long id);
        DateTime DeleteEventType(long id);
        DataTable SaveOrUpdateEventType(long id, string nombre, string descripcion, bool estatus,
            string createdBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
