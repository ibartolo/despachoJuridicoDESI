using Case.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Messages
{
    public class CaseMessages
    {
        public class StatusCaseMessagesResponse
        {
            public List<StatusCaseObj> StatusCaseObjs { get; set; }
            public OperationResult Result { get; set; }
        }

        public class GetStatusCaseByIdRequest
        {
            public long Id { get; set; }
        }

        public class DeleteStatusCaseRequest
        {
            public long Id { get; set; }
        }

        public class SaveOrUpdateStatusCaseRequest
        {
            public StatusCaseObj StatusCase { get; set; }
        }




        public class CaseMessagesResponse
        {
            public List<CaseObj> CaseObjs { get; set; }
            public OperationResult Result { get; set; }
        }

        public class GetCaseByIdRequest
        {
            public long Id { get; set; }
        }

        public class GetCaseByClientIdRequest
        {
            public long ClientId { get; set; }
        }

        public class DeleteCaseRequest
        {
            public long Id { get; set; }
        }

        public class SaveOrUpdateCaseRequest
        {
            public CaseEntity Case { get; set; }
        }

        public class CaseEntity
        {
            public long Id { get; set; }
            public ClientEntity Cliente { get; set; }
            public StatusCaseEntity EstatusCaso { get; set; }
            public string NumeroCaso { get; set; }
            public string Descripcion { get; set; }
            public decimal MontoInicial { get; set; }
            public bool Estatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedDt { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedDt { get; set; }
        }

        public class StatusCaseEntity
        {
            public long Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Orden { get; set; }
            public bool GeneraEvento { get; set; }
            public bool GeneraNotificacion { get; set; }
            public bool Estatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedDt { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedDt { get; set; }
        }

        public class ClientEntity
        {
            public long Id { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Correo { get; set; }
            public bool Estatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedDt { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedDt { get; set; }
        }
    }
}
