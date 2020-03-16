using System.Collections.Generic;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservbusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ComponentViewModel> Components { get; set; }

    }
}
