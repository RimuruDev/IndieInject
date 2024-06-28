using IndieInject;
using UnityEngine;

namespace DIedMoth.Scenes
{
    public class CommonScene : Scene
    {
        public override string GetSceneName()
        {
            return "Scene1";
        }

        public override Camera GetCamera()
        {
            return null;
        }

        protected override void SetupModules()
        {
            Modules.AddModule(new SceneDependenciesModule(new TestSceneProvider()));    
            Modules.AddModule(new SceneAutoInjectModule());    
        }

        protected override void OnSceneLoaded()
        {
            
        }
    }
}