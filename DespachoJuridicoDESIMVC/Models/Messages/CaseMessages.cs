using DespachoJuridicoDESIMVC.Models.Case;
using DespachoJuridicoDESIMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class StatusCaseMessagesResponse
    {
        public List<StatusCaseobj> StatusCaseObjs { get; set; }
        public OperationResult Result { get; set; }
    }

    public class StatusCaseobj
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool GeneraEvento { get; set; }
        public bool GeneraNotificacion { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public object UpdatedDt { get; set; }
    }

    public class CaseMessagesResponse
    {
        public List<CaseObj> CaseObjs { get; set; }
        public OperationResult Result { get; set; }
    }

}