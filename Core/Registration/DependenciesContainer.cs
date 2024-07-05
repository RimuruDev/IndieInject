// **************************************************************** //
//
//   Copyright (c) RimuruDev, YUJECK. All rights reserved.
//   Contact us:
//          - RimuruDev: 
//              - Gmail:    rimuru.dev@gmail.com
//              - LinkedIn: https://www.linkedin.com/in/rimuru/
//              - GitHub:   https://github.com/RimuruDev
//          - YUJECK:
//              - GitHub:   https://github.com/YUJECK
//
//   This project is licensed under the MIT License.
//   See the LICENSE file in the project root for more information.
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieInject
{
    public sealed class DependenciesContainer : IDisposable
    {
        private readonly Dictionary<Type, Dependency> dependencies = new ();

        public void Dispose()
        {
            foreach (var dependencyPair in dependencies)
            {
                dependencyPair.Value.Dispose();
            }
        }

        public Dependency Get<TDependency>()
        {
            return dependencies[typeof(TDependency)];
        }

        public Dependency Get(Type type)
        {
            if (dependencies.TryGetValue(type, out var dependency))
            {
                return dependency;
            }

            return null;
        }

        public void Add(Type dependencyType, Dependency dependency)
        {
            if (!dependencies.TryAdd(dependencyType, dependency))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{dependencyType} DEPENDENCY ALREADY REGISTERED");
#endif
            }
        }

        public void Remove<TDependency>()
        {
            if (dependencies.ContainsKey(typeof(TDependency)))
                dependencies.Remove(typeof(TDependency));
        }

        public void SetupSingletons()
        {
            foreach (var dependencyPair in dependencies)
            {
                if (dependencyPair.Value.IsSingleton)
                {
                    Indie.Injector.Inject(dependencyPair.Value.GetInstance());
                }
            }
        }
    }
}