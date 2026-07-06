using Agenda.Domain;
using Agenda.Proxy;
using Case.Proxy;
using Common.Domain;
using EventType.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application
{
    public class AgendaApp : IAgendaApp
    {
        private readonly IAgendaProxy _proxy;
        private readonly ICaseProxy _caseProxy;
        private readonly IEventTypeProxy _eventTypeProxy;

        public AgendaApp(IAgendaProxy proxy, ICaseProxy caseProxy, IEventTypeProxy eventTypeProxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            _caseProxy = caseProxy ?? throw new ArgumentNullException(nameof(caseProxy));
            _eventTypeProxy = eventTypeProxy ?? throw new ArgumentNullException(nameof(eventTypeProxy));
        }

        private AgendaEntity MapToEntity(AgendaObj obj)
        {
            if (obj == null) return null;

            return new AgendaEntity
            {
                Id = obj.Id,
                CasoId = obj.Caso?.Id ?? 0,
                TipoEventoId = obj.TipoEvento?.Id ?? 0,
                Titulo = obj.Titulo,
                Descripcion = obj.Descripcion,
                FechaInicio = obj.FechaInicio,
                FechaFin = obj.FechaFin,
                GoogleEventId = obj.GoogleEventId,
                SincronizadoGoogle = obj.SincronizadoGoogle,
                FechaSincronizacion = obj.FechaSincronizacion,
                Estatus = obj.Estatus,
                CreatedBy = obj.CreatedBy,
                CreatedDt = obj.CreatedDt,
                UpdatedBy = obj.UpdatedBy,
                UpdatedDt = obj.UpdatedDt,
                NombreCaso = obj.Caso?.NumeroCaso ?? string.Empty,
                NombreTipoEvento = obj.TipoEvento?.Nombre ?? string.Empty
            };
        }

        private List<AgendaEntity> MapToEntityList(List<AgendaObj> objs)
        {
            return objs?.Select(o => MapToEntity(o)).ToList() ?? new List<AgendaEntity>();
        }

        private void PopulateRelatedObjects(AgendaObj agenda)
        {
            if (agenda == null) return;

            if (agenda.Caso != null && agenda.Caso.Id > 0)
            {
                DataTable caseDT = _caseProxy.GetCasoById(agenda.Caso.Id);
                var cases = CaseMapp.MappCaso(caseDT);
                if (cases.Any())
                    agenda.SetCaso(cases.First());
            }

            if (agenda.TipoEvento != null && agenda.TipoEvento.Id > 0)
            {
                DataTable eventTypeDT = _eventTypeProxy.GetEventTypeById(agenda.TipoEvento.Id);
                var eventTypes = EventTypeMapp.MappEventType(eventTypeDT);
                if (eventTypes.Any())
                    agenda.SetTipoEvento(eventTypes.First());
            }
        }

        public List<AgendaEntity> GetAllAgenda(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllAgenda();
                var agendaList = AgendaMapp.MappAgenda(responseDT) ?? new List<AgendaObj>();

                foreach (var agenda in agendaList)
                {
                    PopulateRelatedObjects(agenda);
                }

                return MapToEntityList(agendaList);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los eventos de agenda." }
            };
                return new List<AgendaEntity>();
            }
        }

        public AgendaEntity GetAgendaById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAgendaById(id);
                var agenda = AgendaMapp.MappAgenda(responseDT).FirstOrDefault();

                if (agenda != null)
                {
                    PopulateRelatedObjects(agenda);
                }

                return MapToEntity(agenda);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener evento de agenda por ID." }
            };
                return null;
            }
        }

        public List<AgendaEntity> GetAgendaByCasoId(long casoId, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAgendaByCasoId(casoId);
                var agendaList = AgendaMapp.MappAgenda(responseDT) ?? new List<AgendaObj>();

                foreach (var agenda in agendaList)
                {
                    PopulateRelatedObjects(agenda);
                }

                return MapToEntityList(agendaList);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener eventos de agenda por caso." }
            };
                return new List<AgendaEntity>();
            }
        }

        public List<AgendaEntity> GetAgendaByFecha(DateTime fechaInicio, DateTime fechaFin, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAgendaByFecha(fechaInicio, fechaFin);
                var agendaList = AgendaMapp.MappAgenda(responseDT) ?? new List<AgendaObj>();

                foreach (var agenda in agendaList)
                {
                    PopulateRelatedObjects(agenda);
                }

                return MapToEntityList(agendaList);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener eventos de agenda por fecha." }
            };
                return new List<AgendaEntity>();
            }
        }

        public DateTime DeleteAgenda(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteAgenda(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar evento de agenda." }
            };
                return DateTime.MinValue;
            }
        }

        public AgendaEntity SaveOrUpdateAgenda(AgendaEntity agenda, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateAgenda(
                    agenda.Id,
                    agenda.CasoId,
                    agenda.TipoEventoId,
                    agenda.Titulo,
                    agenda.Descripcion,
                    agenda.FechaInicio,
                    agenda.FechaFin,
                    agenda.GoogleEventId,
                    agenda.SincronizadoGoogle,
                    agenda.FechaSincronizacion,
                    agenda.Estatus,
                    agenda.CreatedBy,
                    agenda.CreatedDt,
                    agenda.UpdatedBy,
                    agenda.UpdatedDt
                );

                var savedAgenda = AgendaMapp.MappAgenda(responseDT).FirstOrDefault();

                if (savedAgenda != null)
                {
                    PopulateRelatedObjects(savedAgenda);
                    return MapToEntity(savedAgenda);
                }

                return null;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar evento de agenda." }
            };
                return null;
            }
        }
    }
}
