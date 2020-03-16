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
                    element.Status = model.Status;
                    element.ConservId = model.ConservId;
                    element.Count = model.Count;
                    element.Sum = model.Sum;
                    element.DateCreate = model.DateCreate;
                    element.DateImplement = model.DateImplement;
                }
                else
                {
                    element = new Order();
                    element.Status = model.Status;
                    element.ConservId = model.ConservId;
                    element.Count = model.Count;
                    element.Sum = model.Sum;
                    element.DateCreate = model.DateCreate;
                    element.DateImplement = model.DateImplement;
                    context.Orders.Add(element);
                }
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
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    Count = rec.Count,
                    ConservName = context.Conservs.FirstOrDefault((r) => r.Id == rec.ConservId).ConservName,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ConservId = rec.ConservId,
                    Status = rec.Status,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}