using System.Collections.Generic;
using DIedMoth.LevelGeneration.GameStates;
using DIedMoth.ServiceLocators;

namespace IndieInject
{
    public class TestSceneProvider : IDependencyProvider
    {
        private double test;

        public TestSceneProvider()
        {
            test = UnityEngine.Random.Range(0, 10);
        }

        [Provide(false)]
        private List<double> ProvideTest1()
        {
            var list = new List<double> {test};
            
            return list;
        }
        
        [Provide(true)]
        private ServiceLocator<GameState> ProvideTest2()
        {
            return new ServiceLocator<GameState>();
        }
    }
}