using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Customer
{
    public class ClientObj
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }
    }

}