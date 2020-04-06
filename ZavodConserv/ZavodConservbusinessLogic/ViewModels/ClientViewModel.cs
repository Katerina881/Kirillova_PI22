using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    class ClientViewModel
    {
        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

    }
}
