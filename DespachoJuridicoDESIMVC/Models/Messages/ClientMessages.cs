using DespachoJuridicoDESIMVC.Models.Common;
using DespachoJuridicoDESIMVC.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class ClientMassagesResponse
    {
        public List<ClienteObj > ClientObjs { get; set; }
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
        public ClienteObj  Cliente { get; set; }
    }
}