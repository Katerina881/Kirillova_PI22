using System;
using System.Collections.Generic;
using System.Text;

namespace ZavodConservbusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int ConservId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
