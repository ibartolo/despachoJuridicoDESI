using Common.Domain;
using Customer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Messages
{
    public class ClientMassagesResponse
    {
        public List<ClientObj> ClientObjs { get; set; }
        public OperationResult Result { get; set; }
    }

    public class GetClienteByIdRequest
    {
        public long Id { get; set; }
    }

    public class GetClienteByCorreoRequest
    {
        public string Correo { get; set; }
    }

    public class DeleteClienteRequest
    {
        public long Id { get; set; }

    }

    public class SaveOrUpdateClienteRequest
    {
        public ClientObj Cliente { get; set; }
    }
}
