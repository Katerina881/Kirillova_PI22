using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
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
        [DisplayName("Исполнитель")]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Консерва")]
        public string ConservName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }

        public override List<string> Properties() => new List<string> { "Id",
"ClientFIO", "ProductName", "ImplementerFIO", "Count", "Sum", "Status", "DateCreate",
"DateImplement" };
    }
}
