using System;
using System.Collections.Generic;
using System.Text;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservbusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
