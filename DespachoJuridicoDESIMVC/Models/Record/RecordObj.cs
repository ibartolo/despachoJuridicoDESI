using DespachoJuridicoDESIMVC.Models.Case;
using DespachoJuridicoDESIMVC.Models.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Record
{
    public class RecordObj
    {
        public long Id { get; set; }
        public CaseObj Case { get; set; } = new CaseObj();
        public CourtObj Court { get; set; } = new CourtObj();
        public string RecordNumber { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDt { get; set; }
        public string Comentarios { get; set; } = string.Empty;
    }
}