using Cysharp.Threading.Tasks;
using DIedMoth.SceneLoader;
using IndieInject;
using UnityEngine;
using System;

namespace DIedMoth.Scenes
{
    public static class Game
    {
        public static bool IsSceneLoaded { get; private set; }
        
        public static Scene CurrentScene { get; private set; }

        public static event Action<Scene> OnSceneStartedLoading; //prev
        public static event Action<Scene, Scene> OnSceneLoaded; //prev/new

        public static readonly IndieInjector Injector = new IndieInjector();
        
        private static readonly AsyncSceneLoader Loader = new();

        public static void SwitchTo<TScene>(TScene scene)
            where TScene : Scene
        {
            IsSceneLoaded = false;
            OnSceneStartedLoading?.Invoke(CurrentScene);

            CurrentScene?.DisposeScene();
            CurrentScene = scene;
            
            scene.InitModules();
            scene.PrepareScene();
            
            Injector.RegisterSceneDependencies(scene);
            
            Loader.LoadScene(scene.GetSceneName(), () => Complete(scene));
        }

        private static void Complete<TScene>(TScene scene) 
            where TScene : Scene
        {
            Scene prevScene = CurrentScene;

            scene.LoadScene();

            IsSceneLoaded = true;
            OnSceneLoaded?.Invoke(prevScene, scene);
        }

        public static async void Tick()
        {
            while (true)
            {
                if (CurrentScene != null && IsSceneLoaded)
                {
                    CurrentScene?.UpdateScene();
                    await UniTask.WaitForFixedUpdate();
                }

                while (!Application.isPlaying)
                    await UniTask.Yield();
            }
        }
    }
}