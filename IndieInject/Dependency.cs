using System;

namespace IndieInject
{
    public sealed class Dependency
    {
        public object Instance;
        public Func<object> Fabric;
        public bool IsSingleton;
    }
}