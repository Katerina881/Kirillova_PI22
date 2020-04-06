using System;
using ZavodConservbusinessLogic.Enums;

namespace ZavodConservbusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }

        public string ConservName { get; set; }

        public string ClientFIO { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }
    }
}
