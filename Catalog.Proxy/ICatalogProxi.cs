using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Proxy
{
    public interface ICatalogProxi
    {
        DataTable SaveOrUpdateTipoDocumento(long Id, string nombre, string descripcion, bool estatus,
            string creayedBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt);
    }
}
