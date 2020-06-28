using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ZavodConservbusinessLogic.Attributes;

namespace ZavodConservbusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 150)]
        [DataMember]
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 100)]
        [DataMember]
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 150)]
        [DataMember]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("Текст")]
        public string Body { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "MessageId",
            "SenderName",
            "DateDelivery",
            "Subject",
            "Body"
        };
    }
}
