using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservDatabaseImplement.Models;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ZavodConservDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id != model.Id);
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }
                element.Status = model.Status;
                element.ConservId = model.ConservId == 0 ? element.ConservId : model.ConservId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                return context.Orders
                 .Where(
                     rec => model == null
                     || (rec.Id == model.Id && model.Id.HasValue)
                     || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                     || (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                 .Include(rec => rec.Conserv)
                 .Select(rec => new OrderViewModel
                 {
                     Id = rec.Id,
                     ConservId = rec.ConservId,
                     ClientId = rec.ClientId,
                     DateCreate = rec.DateCreate,
                     DateImplement = rec.DateImplement,
                     Status = rec.Status,
                     Count = rec.Count,
                     Sum = rec.Sum,
                     ClientFIO = rec.Clients.FirstOrDefault(recC => recC.Id ==
rec.ClientId)?.ClientFIO,
                     ConservName = rec.Conserv.ConservName
                 })
                 .ToList();
            }
        }
    }
}