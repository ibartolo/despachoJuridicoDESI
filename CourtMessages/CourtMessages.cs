using Common.Domain;
using Court.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Messages
{
    public class CourtMessages
    {
        public class CourtMessagesResponse
        {
            public CourtMessagesResponse() 
            {
                CourtObjs = new List<CourtObj>();
                Result = new OperationResult();
            }
            public List<CourtObj> CourtObjs { get; set; }
            public OperationResult Result { get; set; }
        }

        public class GetCourtByIdRequest
        {
            public long Id { get; set; }
        }

        public class DeleteCourtRequest
        {
            public long Id { get; set; }
        }

        public class SaveOrUpdateCourtRequest
        {
            public SaveOrUpdateCourtRequest()
            { 
                Court = new CourtEntity();
            }
            public CourtEntity Court { get; set; }
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
    }
}
