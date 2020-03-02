using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservFileImplement.Models;
using System.Linq;


namespace ZavodConservFileImplement.Implements
{
    public class ConservLogic : IConservLogic
    {
        private readonly FileDataListSingleton source;
        public ConservLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ConservBindingModel model)
        {
            Conserv element = source.Conservs.FirstOrDefault(rec => rec.ConservName ==
           model.ConservName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Conservs.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Conservs.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new Conserv { Id = maxId + 1 };
                source.Conservs.Add(element);
            }
            element.ConservName = model.ConservName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.ConservComponents.RemoveAll(rec => rec.ConservId == model.Id &&
           !model.ConservComponents.ContainsKey(rec.ComponentId));
            // обновили количество у существующих записей
            var updateComponents = source.ConservComponents.Where(rec => rec.ComponentId ==
           model.Id && model.ConservComponents.ContainsKey(rec.ComponentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.ConservComponents[updateComponent.ComponentId].Item2;
                model.ConservComponents.Remove(updateComponent.ComponentId);
            }
            // добавили новые
            int maxPCId = source.ConservComponents.Count > 0 ?
           source.ConservComponents.Max(rec => rec.Id) : 0;
            foreach (var pc in model.ConservComponents)
            {
                source.ConservComponents.Add(new ConservComponent
                {
                    Id = ++maxPCId,
                    ConservId = element.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(ConservBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.ConservComponents.RemoveAll(rec => rec.ConservId == model.Id);
            Conserv element = source.Conservs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Conservs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<ConservViewModel> Read(ConservBindingModel model)
        {
            return source.Conservs
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ConservViewModel
            {
                Id = rec.Id,
                ConservName = rec.ConservName,
                Price = rec.Price,
                ConservComponents = source.ConservComponents
            .Where(recPC => recPC.ConservId == rec.Id)
           .ToDictionary(recPC => recPC.ComponentId, recPC =>
            (source.Components.FirstOrDefault(recC => recC.Id ==
           recPC.ComponentId)?.ComponentName, recPC.Count))
            })
            .ToList();
        }
    }
}
