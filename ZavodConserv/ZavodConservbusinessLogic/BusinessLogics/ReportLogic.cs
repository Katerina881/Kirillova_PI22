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
            var conservs = ConservLogic.Read(null);
            var list = new List<ReportConservComponentViewModel>();

            foreach (var conserv in conservs)
            {
                foreach (var rec in conserv.ConservComponents)
                {
                    var record = new ReportConservComponentViewModel
                    {
                        ConservName = conserv.ConservName,
                        ComponentName = rec.Value.Item1,
                        TotalCount = rec.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список консервов",
                Conserv = ConservLogic.Read(null),
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
                Title = "Список консерв по компонентам",
                ConservComponents = GetConservComponent()
            });
        }
    }
}
