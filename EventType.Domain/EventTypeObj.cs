using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventType.Domain
{
    public class EventTypeObj
    {
        private long _id;
        private string _nombre;
        private string _descripcion;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public string Nombre => _nombre;
        public string Descripcion => _descripcion;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private EventTypeObj(long id)
        {
            _id = id;
            _nombre = string.Empty;
            _descripcion = string.Empty;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static EventTypeObj Create(long id)
        {
            return new EventTypeObj(id);
        }

        public EventTypeObj SetInformacion(string nombre, string descripcion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            return this;
        }

        public EventTypeObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public EventTypeObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public EventTypeObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
