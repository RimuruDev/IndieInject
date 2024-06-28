using DIedMoth.ServiceLocators;

namespace DIedMoth.Scenes.SceneModules
{
    public sealed class SceneModulesManager
    {
        private readonly Scene scene;
        private readonly ServiceLocator<SceneModule> modules = new();

        public SceneModulesManager(Scene scene)
        {
            this.scene = scene;
        }

        public void StartAllModules()
        {
            foreach (var module in modules.GetAll())
            {
                module.StartModule(scene);
            }
        }
        
        public void StopModule<TModule>()
            where TModule : SceneModule
        {
            if (modules.TryGet<TModule>(out var module))
            {
                module.StopModule(scene);
            }
        }
        
        public bool Contains<TModule>()
            where TModule : SceneModule
        {
            return modules.Contains<TModule>();
        }
        
        public bool TryGetModule<TModule>(out TModule module)
            where TModule : SceneModule
        {
            return modules.TryGet(out module);
        }

        public TModule Get<TModule>()
            where TModule : SceneModule
        {
            return modules.Get<TModule>();
        }

        public void UpdateModules()
        {
            foreach (var module in modules.GetAll())
            {
                module.UpdateModule(scene);
            }
        }
        
        public void AddModule<TModule>(TModule module)
            where TModule : SceneModule
        {
            modules.Register(module);
        }

        public void RemoveModule<TModule>()
            where TModule : SceneModule
        {
            if (modules.Unregister<TModule>(out TModule module))
            {   
                module.Dispose(scene);
            }
        }
    }
}