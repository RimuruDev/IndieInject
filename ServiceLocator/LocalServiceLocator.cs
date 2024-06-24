using System;
using System.Collections.Generic;
using UnityEngine;

namespace destructive_code.ServiceLocators
{
    public class LocalServiceLocator<TServiceBase>
        where TServiceBase : class
    {
        private readonly Dictionary<Type, TServiceBase> services = new ();

        public bool Contains<TService>() where TService : TServiceBase => services.ContainsKey(typeof(TService));
        
        public TService Get<TService>() where TService : class, TServiceBase => services[typeof(TService)] as TService;
        
        public bool TryGet<TService>(out TService serviceEnquire)
            where TService : class, TServiceBase
        {
            serviceEnquire = services[typeof(TService)] as TService;
            return serviceEnquire != null;
        }

        public LocalServiceLocator<TServiceBase>Register<TService>(TService service)
            where TService : TServiceBase
        {
            services.TryAdd(service.GetType(), service);
            
            return this;
        }
        
        public LocalServiceLocator<TServiceBase> Unregister<TService>(TService service)
            where TService : TServiceBase
        {
            if (services.ContainsKey(service.GetType()))
                services.Remove(service.GetType());
            
            return this;
        }
    }
}