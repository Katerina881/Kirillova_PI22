using System.Runtime.Serialization;

namespace ZavodConservbusinessLogic.BindingModels
{
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ConservId { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}
