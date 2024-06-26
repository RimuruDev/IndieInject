namespace DIedMoth.ServiceLocators
{
    public sealed class GlobalServiceLocator
    {
        public readonly ServiceLocator<object> GlobalInstances = new ServiceLocator<object>();
        public readonly ServiceLocator<object> SceneInstances = new ServiceLocator<object>();
    }
}