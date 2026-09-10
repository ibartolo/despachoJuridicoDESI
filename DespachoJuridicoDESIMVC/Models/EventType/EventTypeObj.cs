using System;

namespace DespachoJuridicoDESIMVC.Models.EventType
{
    public class EventTypeObj
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDt { get; set; }
    }
}