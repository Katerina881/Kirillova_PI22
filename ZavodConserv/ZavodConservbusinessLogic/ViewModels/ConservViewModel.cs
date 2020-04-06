using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    public class ConservViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название консервы")]
        public string ConservName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> ConservComponents { get; set; }
    }
}
