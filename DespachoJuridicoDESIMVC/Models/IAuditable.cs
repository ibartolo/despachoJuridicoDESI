using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime? CreatedDt { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedDt { get; set; }
    }
}