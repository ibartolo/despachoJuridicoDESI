using System;
using System.Collections.Generic;

namespace DespachoJuridicoDESIMVC.Models.Agenda
{
    public class AgendaObj
    {
        public long Id { get; set; }
        public long CasoId { get; set; }
        public long TipoEventoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string GoogleEventId { get; set; }
        public bool? SincronizadoGoogle { get; set; }
        public DateTime? FechaSincronizacion { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }

        // Propiedades de navegación (para mostrar en el calendario)
        public string NombreCaso { get; set; }
        public string NombreTipoEvento { get; set; }
    }
}