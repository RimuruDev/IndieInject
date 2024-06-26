using System.Collections.Generic;
using DIedMoth.LevelGeneration.GameStates;
using DIedMoth.ServiceLocators;

namespace IndieInject
{
    public class TestSceneProvider : IDependencyProvider
    {
        [Provide(false)]
        private List<double> ProvideTest1()
        {
            var list = new List<double>();
            list.Add(UnityEngine.Random.Range(0, 10));
            return list;
        }
        
        [Provide(true)]
        private ServiceLocator<GameState> ProvideTest2()
        {
            return new ServiceLocator<GameState>();
        }
    }
}