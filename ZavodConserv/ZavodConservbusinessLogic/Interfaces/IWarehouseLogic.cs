using System;
using System.Collections.Generic;
using System.Text;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservbusinessLogic.Interfaces
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> GetList();

        WarehouseViewModel GetElement(int id);

        void AddElement(WarehouseBindingModel model);

        void UpdElement(WarehouseBindingModel model);

        void DelElement(int id);

        void AddComponent(WarehouseComponentBindingModel model);
    }
}
