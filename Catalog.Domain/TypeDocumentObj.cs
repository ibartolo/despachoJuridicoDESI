using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain
{
   public class TypeDocumentObj
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

        private TypeDocumentObj(long DocumentId)
        {
            _id = DocumentId;
            _nombre = string.Empty;
            _descripcion = string.Empty;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;

        }

        public static TypeDocumentObj Create( long DocumentId)
        {
            return new TypeDocumentObj(DocumentId);
        }

        public TypeDocumentObj SetInformacionBasica(string nombre, string descripcion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            return this;
        }
        public TypeDocumentObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public TypeDocumentObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public TypeDocumentObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }

}
