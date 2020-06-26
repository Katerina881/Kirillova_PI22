using System;
using System.Collections.Generic;
using System.Text;
using ZavodConservListImplement.Models;

namespace ZavodConservListImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Component> Components { get; set; }

        public List<Order> Orders { get; set; }

        public List<Conserv> Conservs { get; set; }

        public List<ConservComponent> ConservComponents { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<WarehouseComponent> WarehouseComponents { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>(); 
            Conservs = new List<Conserv>();
            ConservComponents = new List<ConservComponent>();
            Warehouses = new List<Warehouse>();
            WarehouseComponents = new List<WarehouseComponent>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null) 
            { 
                instance = new DataListSingleton(); 
            }
            return instance;
        }
    }
}
