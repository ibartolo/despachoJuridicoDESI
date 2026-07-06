using DespachoJuridicoDESIMVC.Models.Common;
using System;
using System.Collections.Generic;
using DespachoJuridicoDESIMVC.Models.Court;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class SaveOrUpdateCourtRequest
    {
        public CourtObj Court { get; set; }
    }

    public class CourtMassagesResponse
    {
        public List<CourtObj> CourtObjs { get; set; } = new List<CourtObj>();
        public OperationResult Result { get; set; } = new OperationResult();
    }

    public class CourtDeleteResponse
    {
        public DateTime DeletedDt { get; set; }
        public OperationResult Result { get; set; } = new OperationResult();
    }
}