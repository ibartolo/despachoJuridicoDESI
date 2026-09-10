using Common.Domain;
using Dashboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application
{
    public interface IDashboardApp
    {
        DashboardObj GetDashboardData(out OperationResult result);
    }
}
