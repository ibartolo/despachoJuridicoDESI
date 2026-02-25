using Customer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain
{
    public class CaseObj
    {
        private long _id;
        private ClientObj _cliente;
        private StatusCaseObj _estatusCaso;
        private string _numeroCaso;
        private string _descripcion;
        private decimal _montoInicial;
        private bool _estatus;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public ClientObj Cliente => _cliente;
        public StatusCaseObj EstatusCaso => _estatusCaso;
        public string NumeroCaso => _numeroCaso;
        public string Descripcion => _descripcion;
        public decimal MontoInicial => _montoInicial;
        public bool Estatus => _estatus;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private CaseObj(long id)
        {
            _id = id;
            _cliente = null;
            _estatusCaso = null;
            _numeroCaso = string.Empty;
            _descripcion = string.Empty;
            _montoInicial = 0;
            _estatus = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static CaseObj Create(long id)
        {
            return new CaseObj(id);
        }

        public CaseObj SetCliente(ClientObj cliente)
        {
            _cliente = cliente;
            return this;
        }

        public CaseObj SetEstatusCaso(StatusCaseObj estatusCaso)
        {
            _estatusCaso = estatusCaso;
            return this;
        }

        public CaseObj SetInformacionCaso(string numeroCaso, string descripcion, decimal montoInicial)
        {
            _numeroCaso = numeroCaso;
            _descripcion = descripcion;
            _montoInicial = montoInicial;
            return this;
        }

        public CaseObj SetEstatus(bool estatus)
        {
            _estatus = estatus;
            return this;
        }

        public CaseObj SetAuditoriaCreacion(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public CaseObj SetAuditoriaActualizacion(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
