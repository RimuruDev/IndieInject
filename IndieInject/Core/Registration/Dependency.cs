using System;

namespace IndieInject
{
    public sealed class Dependency
    {
        public readonly Type Type;
        public readonly bool IsSingleton;
        
        private readonly Func<object> fabric;
        private object instance;

        public Dependency(Type type, Func<object> fabric, bool isSingleton)
        {
            Type = type;
            this.fabric = fabric;
            IsSingleton = isSingleton;
        }

        public object GetInstance()
        {
            if (IsSingleton && instance == null)
            {
                instance = fabric.Invoke();
            }
            else if (IsSingleton)
            {
                return instance;
            }

            return fabric.Invoke();
        }
    }
}