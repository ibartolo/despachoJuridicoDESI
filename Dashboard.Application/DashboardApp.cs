using Common.Domain;
using Dashboard.Domain;
using Dashboard.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application
{
    public class DashboardApp : IDashboardApp
    {
        private readonly IDashboardProxy _proxy;

        public DashboardApp(IDashboardProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public DashboardObj GetDashboardData(out OperationResult result)
        {
            result = new OperationResult { Successful = true };
            try
            {
                DataSet responseDS = _proxy.GetDashboardData();
                return DashboardMapp.MappDashboard(responseDS);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.SystemMessages = new List<SystemMessage>
            {
                new SystemMessage { Message = "Error al obtener datos del dashboard." }
            };
                return new DashboardObj();
            }
        }
    }
}
