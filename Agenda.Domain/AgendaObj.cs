using Case.Domain;
using EventType.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Domain
{
    public class AgendaObj
    {
        private long _id;
        private CaseObj _caso;
        private EventTypeObj _tipoEvento;
        private string _titulo;
        private string _descripcion;
        private DateTime _fechaInicio;
        private DateTime? _fechaFin;
        private string _googleEventId;
        private bool? _sincronizadoGoogle;
        private DateTime? _fechaSincronizacion;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public CaseObj Caso => _caso;
        public EventTypeObj TipoEvento => _tipoEvento;
        public string Titulo => _titulo;
        public string Descripcion => _descripcion;
        public DateTime FechaInicio => _fechaInicio;
        public DateTime? FechaFin => _fechaFin;
        public string GoogleEventId => _googleEventId;
        public bool? SincronizadoGoogle => _sincronizadoGoogle;
        public DateTime? FechaSincronizacion => _fechaSincronizacion;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private AgendaObj(long id)
        {
            _id = id;
            _caso = null;
            _tipoEvento = null;
            _titulo = string.Empty;
            _descripcion = string.Empty;
            _fechaInicio = DateTime.MinValue;
            _fechaFin = null;
            _googleEventId = string.Empty;
            _sincronizadoGoogle = false;
            _fechaSincronizacion = null;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static AgendaObj Create(long id)
        {
            return new AgendaObj(id);
        }

        public AgendaObj SetCaso(CaseObj caso)
        {
            _caso = caso;
            return this;
        }

        public AgendaObj SetTipoEvento(EventTypeObj tipoEvento)
        {
            _tipoEvento = tipoEvento;
            return this;
        }

        public AgendaObj SetInformacion(string titulo, string descripcion, DateTime fechaInicio, DateTime? fechaFin)
        {
            _titulo = titulo;
            _descripcion = descripcion;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            return this;
        }

        public AgendaObj SetGoogleInfo(string googleEventId, bool? sincronizadoGoogle, DateTime? fechaSincronizacion)
        {
            _googleEventId = googleEventId;
            _sincronizadoGoogle = sincronizadoGoogle;
            _fechaSincronizacion = fechaSincronizacion;
            return this;
        }

        public AgendaObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public AgendaObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public AgendaObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
