using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.BusinessLogics;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservRestApi.Models;

namespace ZavodConservRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IConservLogic _Conserv;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IConservLogic Conserv, MainLogic main)
        {
            _order = order;
            _Conserv = Conserv;
            _main = main;
        }

        [HttpGet]
        public List<ConservModel> GetConservList() => _Conserv.Read(null)?.Select(rec =>
       Convert(rec)).ToList();
        [HttpGet]
        public ConservModel GetConserv(int ConservId) => Convert(_Conserv.Read(new
       ConservBindingModel
        { Id = ConservId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private ConservModel Convert(ConservViewModel model)
        {
            if (model == null) return null;
            return new ConservModel
            {
                Id = model.Id,
                ConservName = model.ConservName,
                Price = model.Price
            };
        }
    }
}
