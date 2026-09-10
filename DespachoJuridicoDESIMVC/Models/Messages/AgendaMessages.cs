using DespachoJuridicoDESIMVC.Models.Agenda;
using DespachoJuridicoDESIMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class AgendaMassagesResponse
    {
        public List<AgendaObj> AgendaItems { get; set; } = new List<AgendaObj>();  // Cambiado de AgendaObjs a AgendaItems
        public OperationResult Result { get; set; } = new OperationResult();
    }

    public class AgendaDeleteResponse
    {
        public DateTime DeletedDt { get; set; }
        public OperationResult Result { get; set; } = new OperationResult();
    }
}