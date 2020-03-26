using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.HelperModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using ZavodConservShopBusinessLogic.BusinessLogics;

namespace ZavodConservbusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentLogic componentLogic;
        private readonly IConservLogic ConservLogic;
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IConservLogic ConservLogic, IComponentLogic componentLogic,
IOrderLogic orderLLogic)
        {
            this.ConservLogic = ConservLogic; 
            this.componentLogic = componentLogic;
            this.orderLogic = orderLLogic;
        }

        public List<ReportConservComponentViewModel> GetConservComponent()
        {
            var components = componentLogic.Read(null);
            var products = ConservLogic.Read(null);
            var list = new List<ReportConservComponentViewModel>();

            foreach (var product in products)
            {
                foreach (var component in components)
                {
                    if (product.ConservComponents.ContainsKey(component.Id))
                    {
                        var record = new ReportConservComponentViewModel
                        {
                            ConservName = product.ConservName,
                            ComponentName = component.ComponentName,
                            TotalCount = product.ConservComponents[component.Id].Item2
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                ConservName = x.ConservName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            }).ToList();
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент и консерв",
                Conserv = ConservLogic.Read(null),
                Components = componentLogic.Read(null)
            });
        }

        public void SaveConservComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        public void SaveConservComponentToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                ConservComponents = GetConservComponent()
            });
        }
    }
}
