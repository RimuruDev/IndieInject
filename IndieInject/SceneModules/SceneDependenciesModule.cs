using DIedMoth.Scenes.SceneModules;

namespace IndieInject
{
    public class SceneDependenciesModule : SceneModule
    {
        public IDependencyProvider[] GetDependencies { get; private set; }

        public SceneDependenciesModule(params IDependencyProvider[] getDependencies)
        {
            GetDependencies = getDependencies;
        }
    }
}