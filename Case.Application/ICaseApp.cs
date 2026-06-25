using Case.Domain;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Case.Messages.CaseMessages;

namespace Case.Application
{
    public interface ICaseApp
    {
        List<StatusCaseObj> GetAllStatusCase(out OperationResult result);
        StatusCaseObj GetStatusCaseById(long id, out OperationResult result);
        DateTime DeleteStatusCase(long id, out OperationResult result);
        StatusCaseObj SaveOrUpdateStatusCase(StatusCaseObj statusCase, out OperationResult result);

        List<CaseObj> GetAllCases(out OperationResult result);
        CaseObj GetCaseById(long id, out OperationResult result);
        List<CaseObj> GetCasesByClientId(long clientId, out OperationResult result);
        DateTime DeleteCase(long id, out OperationResult result);
        CaseObj SaveOrUpdateCase(CaseEntity caseObj, out OperationResult result);
    }
}
