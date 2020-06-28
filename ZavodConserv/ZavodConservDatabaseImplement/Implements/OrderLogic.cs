using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservDatabaseImplement.Models;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ZavodConservbusinessLogic.Enums;

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
                element.ClientId = model.ClientId.Value;
                element.ImplementerId = model.ImplementerId;
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
                return context.Orders.Where(rec => model == null
                     || (rec.Id == model.Id && model.Id.HasValue)
                     || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                     || (model.ClientId.HasValue && rec.ClientId == model.ClientId)
                     || (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue)
                     || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется)
                 )
                 .Select(rec => new OrderViewModel
                 {
                     Id = rec.Id,
                     ClientId = rec.ClientId,
                     ImplementerId = rec.ImplementerId,
                     ConservId = rec.ConservId,
                     DateCreate = rec.DateCreate,
                     DateImplement = rec.DateImplement,
                     Status = rec.Status,
                     Count = rec.Count,
                     Sum = rec.Sum,
                     ConservName = rec.Conserv.ConservName,
                     ClientFIO = rec.Client.FIO,
                     ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty
                 })
                 .ToList();
            }
        }
    }
}