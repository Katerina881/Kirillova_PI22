using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZavodConservbusinessLogic.BindingModels
{
    [DataContract]
    class ClientBindingModel
    {
        [DataMember]
        public int ClientId { get; set; }

    }
}
