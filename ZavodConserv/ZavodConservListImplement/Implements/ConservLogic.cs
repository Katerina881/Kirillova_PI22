using System;
using System.Collections.Generic;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;
using ZavodConservListImplement.Models;

namespace ZavodConservListImplement.Implements
{
    public class ConservLogic : IConservLogic
    {
        private readonly DataListSingleton source;

        public ConservLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ConservBindingModel model)
        {
            Conserv tempConserv = model.Id.HasValue ? null : new Conserv { Id = 1 }; 
            foreach (var Conserv in source.Conservs)
            {
                if (Conserv.ConservName == model.ConservName && Conserv.Id != model.Id)
                {
                    throw new Exception("Уже есть консерва с таким названием"); 
                }
                if (!model.Id.HasValue && Conserv.Id >= tempConserv.Id) 
                {
                    tempConserv.Id = Conserv.Id + 1; 
                }
                else if (model.Id.HasValue && Conserv.Id == model.Id) 
                {
                    tempConserv = Conserv;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempConserv == null) 
                {
                    throw new Exception("Элемент не найден"); 
                }
                CreateModel(model, tempConserv);
            }
            else
            { 
                source.Conservs.Add(CreateModel(model, tempConserv));
            }
        }

        public List<ConservViewModel> Read(ConservBindingModel model)
        {
            List<ConservViewModel> result = new List<ConservViewModel>();
            foreach (var component in source.Conservs)
            {
                if (model != null) 
                { 
                    if (component.Id == model.Id) 
                    { 
                        result.Add(CreateViewModel(component)); 
                        break;
                    } 
                    continue; 
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private ConservViewModel CreateViewModel(Conserv conserv)
        {
            Dictionary<int, (string, int)> ConservComponents = new Dictionary<int, (string, int)>();  
            foreach (var pc in source.ConservComponents)  
            {      
                if (pc.ConservId == conserv.Id)  
                {              
                    string componentName = string.Empty;      
                    foreach (var component in source.Components)      
                    {                
                        if (pc.ComponentId == component.Id)     
                        {                       
                            componentName = component.ComponentName;    
                            break;               
                        }                
                    }               
                    ConservComponents.Add(pc.ComponentId, (componentName, pc.Count));   
                }  
            }       
            return new ConservViewModel      
            {        
                Id = conserv.Id,
                ConservName = conserv.ConservName, 
                Price = conserv.Price,          
                ConservComponents = ConservComponents  
            }; 
        }
        
        public void Delete(ConservBindingModel model)
        {
            for (int i = 0; i < source.ConservComponents.Count; ++i)
            {
                if (source.ConservComponents[i].ConservId == model.Id)
                {
                    source.ConservComponents.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Conservs.Count; ++i)
            {
                if (source.Conservs[i].Id == model.Id)
                {
                    source.Conservs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Conserv CreateModel(ConservBindingModel model, Conserv Conserv)
        {
            Conserv.ConservName = model.ConservName;
            Conserv.Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ConservComponents.Count; ++i)
            {
                if (source.ConservComponents[i].Id > maxPCId)
                {
                    maxPCId = source.ConservComponents[i].Id;
                }
                if (source.ConservComponents[i].ConservId == Conserv.Id)
                {
                    if (model.ConservComponents.ContainsKey(source.ConservComponents[i].ComponentId))
                    {
                        source.ConservComponents[i].Count = model.ConservComponents[source.ConservComponents[i].ComponentId].Item2;

                        model.ConservComponents.Remove(source.ConservComponents[i].ComponentId);
                    }
                    else
                    {
                        source.ConservComponents.RemoveAt(i--);
                    }
                }
            }
            foreach (var pc in model.ConservComponents)
            {
                source.ConservComponents.Add(new ConservComponent
                {
                    Id = ++maxPCId,
                    ConservId = Conserv.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return Conserv;
        }
    }
}
