﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservDatabaseImplement.Models;

namespace ZavodConservDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Email == model.Email && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }

                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }

                element.Email = model.Email;

                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new ZavodConservDatabase())
            {
                return context.Clients
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                    || rec.Email == model.Email && rec.Password == model.Password
                )
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    FIO = rec.FIO,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }
    }
}
