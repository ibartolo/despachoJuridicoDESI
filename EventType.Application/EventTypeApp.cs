using Common.Domain;
using EventType.Domain;
using EventType.Messages;
using EventType.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventType.Application
{
    public class EventTypeApp : IEventTypeApp
    {
        private readonly IEventTypeProxy _proxy;

        public EventTypeApp(IEventTypeProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<EventTypeObj> GetAllEventTypes(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllEventTypes();
                return EventTypeMapp.MappEventType(responseDT) ?? new List<EventTypeObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los tipos de evento." }
            };
                return new List<EventTypeObj>();
            }
        }

        public EventTypeObj GetEventTypeById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetEventTypeById(id);
                return EventTypeMapp.MappEventType(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener tipo de evento por ID." }
            };
                return null;
            }
        }

        public DateTime DeleteEventType(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteEventType(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar tipo de evento." }
            };
                return DateTime.MinValue;
            }
        }

        public EventTypeObj SaveOrUpdateEventType(EventTypeEntity eventType, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateEventType(
                    eventType.Id,
                    eventType.Nombre,
                    eventType.Descripcion,
                    eventType.Estatus,
                    eventType.CreatedBy,
                    eventType.CreatedDt,
                    eventType.UpdatedBy,
                    eventType.UpdatedDt
                );
                return EventTypeMapp.MappEventType(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar tipo de evento." }
            };
                return null;
            }
        }
    }
}
