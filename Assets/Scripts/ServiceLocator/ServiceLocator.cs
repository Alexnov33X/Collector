using System;
using System.Collections.Generic;

public class ServiceLocator<T> : IServiceLocator<T>
{
    protected Dictionary<Type, T> itemsMap { get; }

    public ServiceLocator()
    {
        itemsMap = new Dictionary<Type, T>();
    }

    public TP Register<TP>(TP newService) where TP : T
    {
        var type = newService.GetType();

        if (itemsMap.ContainsKey(type))
        {
            throw new Exception($"KKI. Cannot add item item of type {type}. This type already exist in the Service Locator");
        }
        
        itemsMap[type] = newService;

        return newService;
    }

    public void Unregister<TP>(TP service) where TP : T
    {
        var type = service.GetType();

        if (itemsMap.ContainsKey(type))
        {
            itemsMap.Remove(type);
        }
    }

    public TP Get<TP>() where TP : T
    {
        var type = typeof(TP);

        if (!itemsMap.ContainsKey(type))
        {
            throw new Exception($"KKI. There is no object of type {type} in the Service Locator");
        }

        return (TP)itemsMap[type];
    }
}
