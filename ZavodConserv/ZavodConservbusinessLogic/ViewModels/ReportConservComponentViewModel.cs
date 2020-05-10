using System;
using System.Collections.Generic;

namespace ZavodConservbusinessLogic.ViewModels
{
    public class ReportConservComponentViewModel
    {
        public string ConservName { get; set; }

        public string ComponentName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Conservs { get; set; }

    }
}
