using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain
{
    public class UserObj
    {
        private long _id;
        private string _nombre;
        private string _correo;
        private string _tipoAutenticacion;
        private string _passwordHash;
        private string _proveedorUserId;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public string Nombre => _nombre;
        public string Correo => _correo;
        public string TipoAutenticacion => _tipoAutenticacion;
        public string PasswordHash => _passwordHash;
        public string ProveedorUserId => _proveedorUserId;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private UserObj(long userId)
        {
            _id = userId;
            _nombre = string.Empty;
            _correo = string.Empty;
            _tipoAutenticacion = string.Empty;
            _passwordHash = string.Empty;
            _proveedorUserId = string.Empty;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static UserObj Create(long userId)
        {
            return new UserObj(userId);
        }

        public UserObj SetInformacionBasica(string nombre, string correo)
        {
            _nombre = nombre;
            _correo = correo;
            return this;
        }

        public UserObj SetAutenticacion(string tipoAutenticacion, string passwordHash, string proveedorUserId)
        {
            _tipoAutenticacion = tipoAutenticacion;
            _passwordHash = passwordHash;
            _proveedorUserId = proveedorUserId;
            return this;
        }

        public UserObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public UserObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public UserObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
