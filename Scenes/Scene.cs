using destructive_code.LevelGeneration.GUIWindows;
using IndieInject;
using UnityEngine;

namespace destructive_code.Scenes
{
    public abstract class Scene
    {
        public abstract string GetSceneName();

        public abstract Camera GetCamera();
        public readonly SceneGUI SceneGUI = new SceneGUI();
        public virtual DrankFabric Fabric { get; private set; } = new DrankFabric();

        public IDependencyProvider[] Dependencies { get; }

        public virtual void BeforeSceneLoaded() {}

        public void OnLoaded()
        {
            OnSceneLoaded();
        }
        
        protected abstract void OnSceneLoaded();
        public virtual void Tick() {}
        public virtual void Dispose() {}
    }    
}   