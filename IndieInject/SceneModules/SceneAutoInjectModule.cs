using DIedMoth.Scenes;
using DIedMoth.Scenes.SceneModules;
using UnityEngine;

namespace IndieInject
{
    public class SceneAutoInjectModule : SceneModule
    {
        public override void StartModule(Scene scene)
        {
            var all = GameObject.FindObjectsOfType<MonoBehaviour>(true);

            foreach (var monoBehaviour in all)
            {
                Game.Injector.Inject(monoBehaviour);
            }
        }
    }
}