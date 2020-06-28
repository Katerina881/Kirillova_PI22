using System;
using System.Collections.Generic;
using System.Text;

namespace ZavodConservbusinessLogic.BindingModels
{
    public class ConservBindingModel
    {
        public int? Id { get; set; }
        public string ConservName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ConservComponents { get; set; }
    }
}
