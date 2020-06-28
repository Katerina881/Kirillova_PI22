using System.Collections.Generic;
using System.ComponentModel;
using ZavodConservbusinessLogic.Attributes;

namespace ZavodConservbusinessLogic.ViewModels
{
    public class ComponentViewModel : BaseViewModel
    {
        [Column(title: "Компонент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ComponentName"
        };
    }
}
