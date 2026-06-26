using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
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
}
