using Case.Domain;
using Court.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.Domain
{
    public class RecordObj
    {
        private long _id;
        private CaseObj _case;
        private CourtObj _court;
        private string _recordNumber;
        private string _comentarios;
        private bool _status;
        private string _createdBy;
        private DateTime? _createdDt;
        private string _updatedBy;
        private DateTime? _updatedDt;

        public long Id => _id;
        public CaseObj Case => _case;
        public CourtObj Court => _court;
        public string RecordNumber => _recordNumber;
        public string Comentarios => _comentarios;
        public bool Status => _status;
        public string CreatedBy => _createdBy;
        public DateTime? CreatedDt => _createdDt;
        public string UpdatedBy => _updatedBy;
        public DateTime? UpdatedDt => _updatedDt;

        private RecordObj(long id)
        {
            _id = id;
            _case = null;
            _court = null;
            _recordNumber = string.Empty;
            _comentarios = string.Empty;
            _status = false;
            _createdBy = string.Empty;
            _createdDt = null;
            _updatedBy = string.Empty;
            _updatedDt = null;
        }

        public static RecordObj Create(long id)
        {
            return new RecordObj(id);
        }

        public RecordObj SetCase(CaseObj caseObj)
        {
            _case = caseObj;
            return this;
        }

        public RecordObj SetCourt(CourtObj courtObj)
        {
            _court = courtObj;
            return this;
        }

        public RecordObj SetRecordNumber(string recordNumber)
        {
            _recordNumber = recordNumber;
            return this;
        }

        public RecordObj SetComentarios(string comentarios)
        {
            _comentarios = comentarios;
            return this;
        }

        public RecordObj SetStatus(bool status)
        {
            _status = status;
            return this;
        }

        public RecordObj SetAuditCreation(string createdBy, DateTime createdDt)
        {
            _createdBy = createdBy;
            _createdDt = createdDt;
            return this;
        }

        public RecordObj SetAuditUpdate(string updatedBy, DateTime updatedDt)
        {
            _updatedBy = updatedBy;
            _updatedDt = updatedDt;
            return this;
        }
    }
}
