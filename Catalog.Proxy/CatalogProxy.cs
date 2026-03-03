using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Catalog.Proxy.CatalogProxy;

namespace Catalog.Proxy
{
    public class CatalogProxy : DbWrapper, ICatalogProxi
    {
        public DataTable SaveOrUpdateTipoDocumento(long id,string nombre,string descripcion, bool estatus,
            string creayedBy, DateTime? createdDt, string updatedBy, DateTime? updatedDt)

        {
            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter ("@Id",id),
                new SqlParameter("@Nombre", nombre),
                new SqlParameter("@Descripcion", descripcion),
                new SqlParameter("@Estatus", estatus),
                new SqlParameter("@CreatedBy", creayedBy),
                new SqlParameter("@CreatedDt", createdDt),
                new SqlParameter("@UpdatedBy", updatedBy),
                new SqlParameter("@UpdatedDt", updatedDt)
            };
            return GetObject("SaveOrUpdateTipoDocumento", CommandType.StoredProcedure, sqlParameters);

        }
    }
}
