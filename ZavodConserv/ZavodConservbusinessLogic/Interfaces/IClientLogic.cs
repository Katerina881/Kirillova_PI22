﻿using System;
using System.Collections.Generic;
using System.Text;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservbusinessLogic.BindingModels;

namespace ZavodConservbusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
