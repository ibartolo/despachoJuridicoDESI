using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Customer
{
    public class ClienteObj
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public bool Estatus { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDt { get; set; }
    }

}