using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservbusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportConservComponentViewModel> ConservComponents { get; set; }
    }
}
