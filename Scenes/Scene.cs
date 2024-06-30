using MothDIed.Scenes.SceneModules;
using UnityEngine;

namespace MothDIed.Scenes
{
    public abstract class Scene
    {
        public virtual GameFabric Fabric { get; private set; } = new();
        public SceneModulesManager Modules { get; private set; } 
        
        public abstract string GetSceneName();
        
        public virtual Camera GetCamera()
            => Camera.current;

        public void InitModules()
        {
            Modules = new SceneModulesManager(this);

            SetupModules();
        }

        protected virtual void SetupModules() { }

        public virtual void PrepareScene() { }

        public void LoadScene()
        {
            Modules.StartAllModules();
            OnSceneLoaded();
        }

        protected virtual void OnSceneLoaded() { }

        public virtual void DisposeScene() { }

        public virtual void UpdateScene() { }
    }    
}   