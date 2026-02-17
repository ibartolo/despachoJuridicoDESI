using Case.Domain;
using Case.Proxy;
using Common.Domain;
using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application
{
    public class CaseApp : ICaseApp
    {
        private readonly IStatusCaseProxy _proxy;

        public CaseApp(IStatusCaseProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<StatusCaseObj> GetAllStatusCase(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllEstatusCaso(); // CORRECTO
                return StatusCaseMapp.MappEstatusCaso(responseDT) ?? new List<StatusCaseObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener catálogo de estatus de caso." }
            };
                return new List<StatusCaseObj>();
            }
        }

        public StatusCaseObj GetStatusCaseById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetEstatusCasoById(id); // CORRECTO
                return StatusCaseMapp.MappEstatusCaso(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener estatus de caso por ID." }
            };
                return null;
            }
        }

        public DateTime DeleteStatusCase(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteEstatusCaso(id); // CORRECTO
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar estatus de caso." }
            };
                return DateTime.MinValue;
            }
        }

        public StatusCaseObj SaveOrUpdateStatusCase(StatusCaseObj statusCase, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateEstatusCaso( // CORRECTO
                    statusCase.Id,
                    statusCase.Nombre,
                    statusCase.Descripcion,
                    statusCase.Orden,
                    statusCase.Estatus,
                    statusCase.CreatedBy,
                    statusCase.CreatedDt,
                    statusCase.UpdatedBy,
                    statusCase.UpdatedDt
                );
                return StatusCaseMapp.MappEstatusCaso(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar estatus de caso." }
            };
                return null;
            }
        }
    }
}
