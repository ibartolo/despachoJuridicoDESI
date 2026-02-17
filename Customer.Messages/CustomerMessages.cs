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
        public ClientEntity Cliente { get; set; }
    }
    public class ClientEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }
    }
}
