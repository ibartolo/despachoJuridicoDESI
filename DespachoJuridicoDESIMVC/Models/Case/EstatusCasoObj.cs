using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Case
{
    public class EstatusCasoObj
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Orden { get; set; }
        public bool GeneraEvento { get; set; }
        public bool GeneraNotificacion { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDt { get; set; }
    }
}