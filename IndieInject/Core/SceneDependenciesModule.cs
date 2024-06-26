﻿using DIedMoth.Scenes;
using DIedMoth.Scenes.SceneModules;
using UnityEngine;

namespace IndieInject
{
    public class SceneDependenciesModule : SceneModule
    {
        public IDependencyProvider[] GetDependencies { get; private set; }

        private bool autoInjectInSceneGameObjects;
        
        public SceneDependenciesModule(bool autoInjectInSceneGameObjects, params IDependencyProvider[] getDependencies)
        {
            this.autoInjectInSceneGameObjects = autoInjectInSceneGameObjects;
            
            GetDependencies = getDependencies;
        }

        public override void StartModule(Scene scene)
        {
            Debug.Log("Start Module");
            
            var all = GameObject.FindObjectsOfType<MonoBehaviour>(true);

            foreach (var monoBehaviour in all)
            {
                Game.Injector.Inject(monoBehaviour);
            }
        }
    }
}