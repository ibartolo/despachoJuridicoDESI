using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Domain
{
    public class CourtObj
    {
        private long _id;
        private string _nombre;
        private string _direccion;
        private string _colonia;
        private string _ciudad;
        private string _estado;
        private string _codigoPostal;
        private string _telefono;
        private string _horario;
        private string _observaciones;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public string Nombre => _nombre;
        public string Direccion => _direccion;
        public string Colonia => _colonia;
        public string Ciudad => _ciudad;
        public string Estado => _estado;
        public string CodigoPostal => _codigoPostal;
        public string Telefono => _telefono;
        public string Horario => _horario;
        public string Observaciones => _observaciones;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private CourtObj(long id)
        {
            _id = id;
            _nombre = string.Empty;
            _direccion = string.Empty;
            _colonia = string.Empty;
            _ciudad = string.Empty;
            _estado = string.Empty;
            _codigoPostal = string.Empty;
            _telefono = string.Empty;
            _horario = string.Empty;
            _observaciones = string.Empty;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static CourtObj Create(long id)
        {
            return new CourtObj(id);
        }

        public CourtObj SetInformacionBasica(string nombre, string direccion, string colonia, string ciudad, string estado, string codigoPostal)
        {
            _nombre = nombre;
            _direccion = direccion;
            _colonia = colonia;
            _ciudad = ciudad;
            _estado = estado;
            _codigoPostal = codigoPostal;
            return this;
        }

        public CourtObj SetContacto(string telefono, string horario)
        {
            _telefono = telefono;
            _horario = horario;
            return this;
        }

        public CourtObj SetObservaciones(string observaciones)
        {
            _observaciones = observaciones;
            return this;
        }

        public CourtObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public CourtObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public CourtObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
