using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain
{
    public class ClientObj
    {
        private long _id;
        private string _nombre;
        private string _telefono;
        private string _correo;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public string Nombre => _nombre;
        public string Telefono => _telefono;
        public string Correo => _correo;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private ClientObj(long id)
        {
            _id = id;
            _nombre = string.Empty;
            _telefono = string.Empty;
            _correo = string.Empty;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static ClientObj Create(long id)
        {
            return new ClientObj(id);
        }

        public ClientObj SetInformacionBasica(string nombre, string telefono, string correo)
        {
            _nombre = nombre;
            _telefono = telefono;
            _correo = correo;
            return this;
        }

        public ClientObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public ClientObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public ClientObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
