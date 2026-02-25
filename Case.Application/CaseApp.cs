using Case.Domain;
using Case.Proxy;
using Common.Domain;
using Customer.Proxy;
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
        private readonly IStatusCaseProxy _statusProxy;
        private readonly ICaseProxy _caseProxy;
        private readonly IClientProxy _clientProxy;

        public CaseApp(IStatusCaseProxy statusProxy, ICaseProxy caseProxy, IClientProxy clientProxy)
        {
            _statusProxy = statusProxy ?? throw new ArgumentNullException(nameof(statusProxy));
            _caseProxy = caseProxy;
            _clientProxy = clientProxy;
        }
        public List<StatusCaseObj> GetAllStatusCase(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _statusProxy.GetAllEstatusCaso(); // CORRECTO
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
                DataTable responseDT = _statusProxy.GetEstatusCasoById(id); // CORRECTO
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
                return _statusProxy.DeleteEstatusCaso(id); // CORRECTO
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
                DataTable responseDT = _statusProxy.SaveOrUpdateEstatusCaso( // CORRECTO
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



        public List<CaseObj> GetAllCases(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _caseProxy.GetAllCaso();
                var cases = CaseMapp.MappCaso(responseDT) ?? new List<CaseObj>();

                // Poblar objetos StatusCase
                foreach (var caseObj in cases)
                {
                    if (caseObj.EstatusCaso != null && caseObj.EstatusCaso.Id > 0)
                    {
                        DataTable statusDT = _statusProxy.GetEstatusCasoById(caseObj.EstatusCaso.Id);
                        var statusList = StatusCaseMapp.MappEstatusCaso(statusDT);
                        if (statusList.Any())
                        {
                            caseObj.SetEstatusCaso(statusList.First());
                        }
                    }

                    if(caseObj.Cliente != null && caseObj.Cliente.Id > 0)
                    {
                        DataTable clienteDT = _clientProxy.GetClienteById(caseObj.Cliente.Id);
                        var clienteList = ClientMapp.MappCliente(clienteDT);
                        if (clienteList.Any())
                        {
                            caseObj.SetCliente(clienteList.First());
                        }
                    }
                }

                return cases;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los casos." }
            };
                return new List<CaseObj>();
            }
        }
        public CaseObj GetCaseById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _caseProxy.GetCasoById(id);
                var cases = CaseMapp.MappCaso(responseDT);
                var caseObj = cases.FirstOrDefault();

                if (caseObj != null && caseObj.EstatusCaso != null && caseObj.EstatusCaso.Id > 0)
                {
                    DataTable statusDT = _statusProxy.GetEstatusCasoById(caseObj.EstatusCaso.Id);
                    var statusList = StatusCaseMapp.MappEstatusCaso(statusDT);
                    if (statusList.Any())
                    {
                        caseObj.SetEstatusCaso(statusList.First());
                    }
                }

                return caseObj;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener caso por ID." }
            };
                return null;
            }
        }
        public List<CaseObj> GetCasesByClientId(long clientId, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _caseProxy.GetCasoByClienteId(clientId);
                var cases = CaseMapp.MappCaso(responseDT) ?? new List<CaseObj>();

                // Poblar objetos StatusCase
                foreach (var caseObj in cases)
                {
                    if (caseObj.EstatusCaso != null && caseObj.EstatusCaso.Id > 0)
                    {
                        DataTable statusDT = _statusProxy.GetEstatusCasoById(caseObj.EstatusCaso.Id);
                        var statusList = StatusCaseMapp.MappEstatusCaso(statusDT);
                        if (statusList.Any())
                        {
                            caseObj.SetEstatusCaso(statusList.First());
                        }
                    }
                }

                return cases;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener casos por cliente." }
            };
                return new List<CaseObj>();
            }
        }
        public DateTime DeleteCase(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _caseProxy.DeleteCaso(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar caso." }
            };
                return DateTime.MinValue;
            }
        }
        public CaseObj SaveOrUpdateCase(CaseObj caseObj, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                long estatusCasoId = caseObj.EstatusCaso?.Id ?? 0;

                DataTable responseDT = _caseProxy.SaveOrUpdateCaso(
                    caseObj.Id,
                    caseObj.Cliente?.Id ?? 0,
                    estatusCasoId,
                    caseObj.NumeroCaso,
                    caseObj.Descripcion,
                    caseObj.MontoInicial,
                    caseObj.Estatus,
                    caseObj.CreatedBy,
                    caseObj.CreatedDt,
                    caseObj.UpdatedBy,
                    caseObj.UpdatedDt
                );

                var cases = CaseMapp.MappCaso(responseDT);
                var savedCase = cases.FirstOrDefault();

                if (savedCase != null && savedCase.EstatusCaso != null && savedCase.EstatusCaso.Id > 0)
                {
                    DataTable statusDT = _statusProxy.GetEstatusCasoById(savedCase.EstatusCaso.Id);
                    var statusList = StatusCaseMapp.MappEstatusCaso(statusDT);
                    if (statusList.Any())
                    {
                        savedCase.SetEstatusCaso(statusList.First());
                    }
                }

                return savedCase;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar caso." }
            };
                return null;
            }
        }
    }
}
