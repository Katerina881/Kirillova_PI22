using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.HelperModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            this.componentLogic = componentLogic; this.orderLogic = orderLLogic;
        }

        public List<ReportConservComponentViewModel> GetConservComponent()
        {
            var components = componentLogic.Read(null);

            var Conservs = ConservLogic.Read(null);

            var list = new List<ReportConservComponentViewModel>();

            foreach (var component in components)
            {
                var record = new ReportConservComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Conservs = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var Conserv in Conservs)
                {
                    if (Conserv.ConservComponents.ContainsKey(component.Id))
                    {
                        record.Conservs.Add(new Tuple<string, int>(Conserv.ConservName, Conserv.ConservComponents[component.Id].Item2)); record.TotalCount += Conserv.ConservComponents[component.Id].Item2;
                    }
                }

                list.Add(record);
            }

            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                    .Select(x => new ReportOrdersViewModel
                    {
                        DateCreate = x.DateCreate,
                        ConservName = x.ConservName,
                        Count = x.Count,
                        Sum = x.Sum,
                        Status = x.Status
                    })
                    .ToList();
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Components = componentLogic.Read(null)
            });
        }

        public void SaveConservComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                ConservComponents = GetConservComponent()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
