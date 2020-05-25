using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ZavodConservbusinessLogic.Attributes;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    public class ConservViewModel : BaseViewModel
    {
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ConservName { get; set; }

        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> ConservComponents { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ProductName",
            "Price"
        };
    }
}
