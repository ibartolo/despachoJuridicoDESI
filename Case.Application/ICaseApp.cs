using Case.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application
{
    public interface ICaseApp
    {
        List<StatusCaseObj> GetAllStatusCase(out OperationResult result);
        StatusCaseObj GetStatusCaseById(long id, out OperationResult result);
        DateTime DeleteStatusCase(long id, out OperationResult result);
        StatusCaseObj SaveOrUpdateStatusCase(StatusCaseObj statusCase, out OperationResult result);
    }
}
