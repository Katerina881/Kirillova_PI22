using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservDatabaseImplement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ZavodConservDatabaseImplement.Implements
{
    public class ConservLogic : IConservLogic
    {
        public void CreateOrUpdate(ConservBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Conserv element = context.Conservs.FirstOrDefault(rec =>
                       rec.ConservName == model.ConservName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть консерва с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Conservs.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Conserv();
                            context.Conservs.Add(element);
                        }
                        element.ConservName = model.ConservName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var conservComponents = context.ConservComponents.Where(rec
                           => rec.ConservId == model.Id.Value).ToList();
                            context.ConservComponents.RemoveRange(conservComponents.Where(rec =>
                            !model.ConservComponents.ContainsKey(rec.ComponentId)).ToList());
                            context.SaveChanges();
                            foreach (var updateComponent in conservComponents)
                            {
                                updateComponent.Count =
                               model.ConservComponents[updateComponent.ComponentId].Item2;

                                model.ConservComponents.Remove(updateComponent.ComponentId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.ConservComponents)
                        {
                            context.ConservComponents.Add(new ConservComponent
                            {
                                ConservId = element.Id,
                                ComponentId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(ConservBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.ConservComponents.RemoveRange(context.ConservComponents.Where(rec =>
                        rec.ConservId == model.Id));
                        Conserv element = context.Conservs.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Conservs.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<ConservViewModel> Read(ConservBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                return context.Conservs
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new ConservViewModel
               {
                   Id = rec.Id,
                   ConservName = rec.ConservName,
                   Price = rec.Price,
                   ConservComponents = context.ConservComponents
                .Include(recPC => recPC.Component)
               .Where(recPC => recPC.ConservId == rec.Id)
               .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
