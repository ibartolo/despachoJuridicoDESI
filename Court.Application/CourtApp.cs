using Common.Domain;
using Court.Domain;
using Court.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Court.Messages.CourtMessages;

namespace Court.Application
{
    public class CourtApp : ICourtApp
    {
        private readonly ICourtProxy _proxy;

        public CourtApp(ICourtProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public List<CourtObj> GetAllCourts(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetAllCourts();
                return CourtMapp.MappCourt(responseDT) ?? new List<CourtObj>();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener todos los juzgados." }
            };
                return new List<CourtObj>();
            }
        }

        public CourtObj GetCourtById(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.GetCourtById(id);
                return CourtMapp.MappCourt(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener juzgado por ID." }
            };
                return null;
            }
        }

        public DateTime DeleteCourt(long id, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                return _proxy.DeleteCourt(id);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al eliminar juzgado." }
            };
                return DateTime.MinValue;
            }
        }

        public CourtObj SaveOrUpdateCourt(CourtEntity court, out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataTable responseDT = _proxy.SaveOrUpdateCourt(
                    court.Id,
                    court.Nombre,
                    court.Direccion,
                    court.Colonia,
                    court.Ciudad,
                    court.Estado,
                    court.CodigoPostal,
                    court.Telefono,
                    court.Horario,
                    court.Observaciones,
                    court.Estatus,
                    court.CreatedBy,
                    court.CreatedDt,
                    court.UpdatedBy,
                    court.UpdatedDt
                );
                return CourtMapp.MappCourt(responseDT).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al guardar/actualizar juzgado." }
            };
                return null;
            }
        }
    }
}
