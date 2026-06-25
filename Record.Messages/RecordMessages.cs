using Common.Domain;
using Record.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Messages
{
    public class RecordMessagesResponse
    {
        public RecordMessagesResponse() 
        {
            Result = new OperationResult();
            RecordObjs = new List<RecordObj>();
        }
        public List<RecordObj> RecordObjs { get; set; }
        public OperationResult Result { get; set; }
    }

    public class GetRecordByCaseIdRequest
    {
        public long CaseId { get; set; }
    }

    public class GetRecordByIdRequest
    {
        public long Id { get; set; }
    }

    public class DeleteRecordRequest
    {
        public long Id { get; set; }
    }

    public class SaveOrUpdateRecordRequest
    {
        public RecordEntity Record { get; set; }
    }

    public class RecordEntity
    {
        public long Id { get; set; }
        public CaseEntity Case { get; set; }
        public CourtEntity Court { get; set; }
        public string RecordNumber { get; set; }
        public string Comentarios { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }
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

    public class CourtEntity
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Horario { get; set; }
        public string Observaciones { get; set; }
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
}
