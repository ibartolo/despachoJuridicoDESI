using DespachoJuridicoDESIMVC.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Case
{
    public class CaseObj
    {
        public long Id { get; set; }
        public ClienteObj Cliente { get; set; } = new ClienteObj();
        public EstatusCasoObj EstatusCaso { get; set; }  // ← Cambiado a nullable
        public string NumeroCaso { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal MontoInicial { get; set; }
        public bool Estatus { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDt { get; set; }
    }
}