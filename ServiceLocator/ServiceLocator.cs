using System;
using System.Collections.Generic;

namespace DIedMoth.ServiceLocators
{
    public class ServiceLocator<TServiceBase>
        where TServiceBase : class
    {
        private readonly Dictionary<Type, TServiceBase> services = new ();
        private readonly List<TServiceBase> servicesList = new ();

        public TServiceBase[] GetAll() => servicesList.ToArray();
        
        public bool Contains<TService>() where TService : TServiceBase => services.ContainsKey(typeof(TService));
        
        public TService Get<TService>() where TService : class, TServiceBase => services[typeof(TService)] as TService;
        
        public bool TryGet<TService>(out TService serviceEnquire)
            where TService : class, TServiceBase
        {
            serviceEnquire = services[typeof(TService)] as TService;
            return serviceEnquire != null;
        }

        public ServiceLocator<TServiceBase>Register<TService>(TService service)
            where TService : TServiceBase
        {
            if(services.TryAdd(service.GetType(), service))
                servicesList.Add(service);

            return this;
        }
        
        public bool Unregister<TService>()
            where TService : TServiceBase
        {
            if (services.TryGetValue(typeof(TService), out TServiceBase service))
            {
                services.Remove(typeof(TService));
                servicesList.Remove(service);
                
                return true;
            }

            return false;
        }
        public bool Unregister<TService>(out TService returnService)
            where TService : class, TServiceBase
        {
            if (services.TryGetValue(typeof(TService), out TServiceBase service))
            {
                services.Remove(typeof(TService));
                servicesList.Remove(service);
                
                returnService = service as TService;

                return true;
            }

            returnService = null;
            return false;
        }
    }
}