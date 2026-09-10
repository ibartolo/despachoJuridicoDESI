using Common.Domain;
using Dashboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Messages
{
    public class DashboardMessagesResponse
    {
        public DashboardObj Dashboard { get; set; }
        public OperationResult Result { get; set; }

        public DashboardMessagesResponse()
        {
            Dashboard = new DashboardObj();
            Result = new OperationResult();
        }
    }
}
