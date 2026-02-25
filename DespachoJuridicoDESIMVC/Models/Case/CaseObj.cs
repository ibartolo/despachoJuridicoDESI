using DespachoJuridicoDESIMVC.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Case
{
    public class CaseObj
    {
        public CaseObj()
        {
            Cliente = new ClientObj();
            EstatusCaso = new StatusCaseObj();
        }
        public long Id { get; set; }
        public ClientObj Cliente { get; set; }
        public StatusCaseObj EstatusCaso { get; set; }
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