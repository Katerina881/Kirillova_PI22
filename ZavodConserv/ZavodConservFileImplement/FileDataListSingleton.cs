using ZavodConservbusinessLogic.Enums;
using ZavodConservFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace ZavodConservFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ConservFileName = "Conserv.xml";
        private readonly string ConservComponentFileName = "ConservComponent.xml";
        private readonly string ClientFileName = "Client.xml";

        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Conserv> Conservs { get; set; }
        public List<ConservComponent> ConservComponents { get; set; }
        public List<Client> Clients { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Conservs = LoadConservs();
            ConservComponents = LoadConservComponents();
            Clients = LoadClients();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveConservs();
            SaveConservComponents();
            SaveClients();
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        ConservId = Convert.ToInt32(elem.Element("ConservId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }

        private List<Conserv> LoadConservs()
        {
            var list = new List<Conserv>();
            if (File.Exists(ConservFileName))
            {
             XDocument xDocument = XDocument.Load(ConservFileName);
                var xElements = xDocument.Root.Elements("Conserv").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Conserv
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ConservName = elem.Element("ConservName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }

        private List<ConservComponent> LoadConservComponents()
        {
            var list = new List<ConservComponent>();
            if (File.Exists(ConservComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ConservComponentFileName);
                var xElements = xDocument.Root.Elements("ConservComponent").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new ConservComponent
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ConservId = Convert.ToInt32(elem.Element("ConservId").Value),
                        ComponentId = Convert.ToInt32(elem.Element("ComponentId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }

            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("ClientId", order.ClientId),
                    new XElement("ConservId", order.ConservId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveConservs()
        {
            if (Conservs != null)
            {
                var xElement = new XElement("Conservs");
                foreach (var conserv in Conservs)
                {
                    xElement.Add(new XElement("Conserv",
                    new XAttribute("Id", conserv.Id),
                    new XElement("ConservName", conserv.ConservName),
                    new XElement("Price", conserv.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ConservFileName);
            }
        }
        private void SaveConservComponents()
        {
            if (ConservComponents != null)
            {
                var xElement = new XElement("ConservComponents");
                foreach (var conservComponent in ConservComponents)
                {
                    xElement.Add(new XElement("ConservComponent",
                    new XAttribute("Id", conservComponent.Id),
                    new XElement("ConservId", conservComponent.ConservId),
                    new XElement("ComponentId", conservComponent.ComponentId),
                    new XElement("Count", conservComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ConservComponentFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("FIO", client.FIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}

