using DespachoJuridicoDESIMVC.Models.Autentication;
using DespachoJuridicoDESIMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DespachoJuridicoDESIMVC.Models.Messages
{
    public class UserMassages
    {
        public List<UserObj> UserObjs { get; set; }
        public OperationResult Result { get; set; }
    }
}