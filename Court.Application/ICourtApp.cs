using Common.Domain;
using Court.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Court.Application
{
    public interface ICourtApp
    {
        List<CourtObj> GetAllCourts(out OperationResult result);
        CourtObj GetCourtById(long id, out OperationResult result);
        DateTime DeleteCourt(long id, out OperationResult result);
        CourtObj SaveOrUpdateCourt(CourtObj court, out OperationResult result);
    }
}
