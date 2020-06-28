using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ZavodConservbusinessLogic.Attributes;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    public abstract class BaseViewModel
    {
        [Column(visible: false)]
        [DataMember]
        public int Id { get; set; }
        public abstract List<string> Properties();
    }
}
