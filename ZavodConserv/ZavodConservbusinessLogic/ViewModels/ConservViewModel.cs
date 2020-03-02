using System.Collections.Generic;
using System.ComponentModel;

namespace ZavodConservbusinessLogic.ViewModels
{
    public class ConservViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название консервы")]
        public string ConservName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ConservComponents { get; set; }
    }
}
