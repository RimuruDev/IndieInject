using System;

namespace IndieInject
{
    public sealed class Dependency
    {
        public readonly Type Type;
        public readonly Func<object> Fabric;
        public readonly bool IsSingleton;

        private object instance;

        public Dependency(Type type, Func<object> fabric, bool isSingleton)
        {
            Type = type;
            Fabric = fabric;
            IsSingleton = isSingleton;
        }

        public object GetInstance()
        {
            if (IsSingleton && instance == null)
            {
                instance = Fabric.Invoke();
            }
            else if (IsSingleton)
            {
                return instance;
            }

            return Fabric.Invoke();
        }
    }
}