using SqlProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Proxy
{
    public class DashboardProxy : DbWrapper, IDashboardProxy
    {
        public DataSet GetDashboardData()
        {
            return GetDataSet("GetDashboardData", CommandType.StoredProcedure);
        }
    }
}
