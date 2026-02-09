using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain
{
    public class StatusCaseObj
    {
        private long _id;
        private string _nombre;
        private string _descripcion;
        private int _orden;
        private bool _generaEvento;
        private bool _generaNotificacion;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public string Nombre => _nombre;
        public string Descripcion => _descripcion;
        public int Orden => _orden;
        public bool GeneraEvento => _generaEvento;
        public bool GeneraNotificacion => _generaNotificacion;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private StatusCaseObj(long id)
        {
            _id = id;
            _nombre = string.Empty;
            _descripcion = string.Empty;
            _orden = 0;
            _generaEvento = false;
            _generaNotificacion = false;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static StatusCaseObj Create(long id)
        {
            return new StatusCaseObj(id);
        }

        public StatusCaseObj SetInformacionBasica(string nombre, string descripcion, int orden)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            _orden = orden;
            return this;
        }

        public StatusCaseObj SetOpciones(bool generaEvento, bool generaNotificacion)
        {
            _generaEvento = generaEvento;
            _generaNotificacion = generaNotificacion;
            return this;
        }

        public StatusCaseObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public StatusCaseObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public StatusCaseObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
