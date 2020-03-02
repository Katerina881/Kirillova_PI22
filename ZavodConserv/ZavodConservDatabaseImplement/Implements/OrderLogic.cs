using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservDatabaseImplement.Models;
using System.Linq;

namespace ZavodConservDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec =>
rec.OrderName == model.OrderName && rec.Id != model.Id);
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
                    element = new Order ();
                    context.Orders.Add(element);
                }

                element.Status = model.Status;
                element.ConservId = model.ConservId == 0 ? element.ConservId : model.ConservId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
            }
            
        }

        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                Count = rec.Count,
                ConservName = GetConservName(rec.ConservId),
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                ConservId = rec.ConservId,
                Status = rec.Status,
                Sum = rec.Sum
            })
            .ToList();
        }

        private string GetConservName(int id)
        {
            string name = "";
            var conserv = source.Conservs.FirstOrDefault(x => x.Id == id);
            name = conserv != null ? conserv.ConservName : "";
            return name;
        }
    }
}
