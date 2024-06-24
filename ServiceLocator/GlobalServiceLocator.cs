namespace destructive_code.ServiceLocators
{
    public sealed class GlobalServiceLocator
    {
        public readonly LocalServiceLocator<object> GlobalInstances = new LocalServiceLocator<object>();
        public readonly LocalServiceLocator<object> SceneInstances = new LocalServiceLocator<object>();
    }
}