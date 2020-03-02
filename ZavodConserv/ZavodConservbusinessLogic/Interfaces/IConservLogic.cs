using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservbusinessLogic.Interfaces
{
    public interface IConservLogic
    {
        List<ConservViewModel> Read(ConservBindingModel model);
        void CreateOrUpdate(ConservBindingModel model);
        void Delete(ConservBindingModel model);
    }
}
