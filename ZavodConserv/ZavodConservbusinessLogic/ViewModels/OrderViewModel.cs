using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ZavodConservbusinessLogic.Attributes;
using ZavodConservbusinessLogic.Enums;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int ConservId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        [Column(title: "Исполнитель", width: 70)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        [Column(title: "Консерва", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ConservName { get; set; }

        [Column(title: "Количество", width: 100)]
        [DataMember]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 80)]
        [DataMember]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        [DataMember]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        [DataMember]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        [DataMember]
        public DateTime? DateImplement { get; set; }

        public override List<string> Properties() => new List<string> { "Id",
"ClientFIO", "ProductName", "ImplementerFIO", "Count", "Sum", "Status", "DateCreate",
"DateImplement" };
    }
}
