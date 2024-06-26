using DIedMoth.Scenes.SceneModules;
using UnityEngine;

namespace DIedMoth.Scenes
{
    public abstract class Scene
    {
        public virtual DrankFabric Fabric { get; private set; } = new();
        public SceneModulesManager Modules { get; private set; } 
        
        public abstract string GetSceneName();
        
        public abstract Camera GetCamera();

        public void InitModules()
        {
            Modules = new SceneModulesManager(this);

            SetupModules();
        }

        protected virtual void SetupModules() { }

        public virtual void PrepareScene() { }

        public void LoadScene()
        {
            Debug.Log("Scene loaded " + GetSceneName());

            Modules.StartAllModules();
            OnSceneLoaded();
        }

        protected virtual void OnSceneLoaded() { }

        public virtual void DisposeScene() { }

        public virtual void UpdateScene() { }
    }    
}   