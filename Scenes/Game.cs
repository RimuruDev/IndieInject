using System;
using Cysharp.Threading.Tasks;
using destructive_code.LevelGeneration;
using destructive_code.SceneLoader;
using destructive_code.ServiceLocators;
using IndieInject;
using UnityEngine;

namespace destructive_code.Scenes
{
    public static class Game
    {
        public static bool IsSceneLoaded { get; private set; }
        
        public static Scene CurrentScene { get; private set; }

        public static event Action<Scene> OnSceneStartedLoading; //prev
        public static event Action<Scene, Scene> OnSceneLoaded; //prev/new

        public static readonly IndieInjector Injector = new IndieInjector();
        
        private static readonly AsyncSceneLoader Loader = new();

        static Game()
        {
            Tick();
        }

        public static void SwitchTo<TScene>(TScene scene)
            where TScene : Scene
        {
            IsSceneLoaded = false;
            OnSceneStartedLoading?.Invoke(CurrentScene);

            CurrentScene?.Dispose();
            CurrentScene = scene;
            
            Injector.RegisterSceneDependencies(scene);
            scene.BeforeSceneLoaded();
            
            Loader.LoadScene(scene.GetSceneName(), () => Complete(scene));
        }

        private static void Complete<TScene>(TScene scene) 
            where TScene : Scene
        {
            Scene prevScene = CurrentScene;

            scene.OnLoaded();

            IsSceneLoaded = true;
            OnSceneLoaded?.Invoke(prevScene, scene);
        }

        public static async void Tick()
        {
            while (true)
            {
                if (CurrentScene != null && IsSceneLoaded)
                {
                    CurrentScene?.Tick();
                    await UniTask.WaitForFixedUpdate();
                }

                while (!Application.isPlaying)
                    await UniTask.Yield();
            }
        }
    }
}