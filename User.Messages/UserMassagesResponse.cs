using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;

namespace User.Messages
{
    public class UserMassagesResponse
    {
        public List<UserObj> UserObjs { get; set; }
        public OperationResult Result { get; set; }
    }
    public class UserMassagesRequest
    {
        public string email { get; set; }
    }
}
