using System;
using System.Collections.Generic;
using IndieInject;
using UnityEngine;

namespace IndieInject
{
    public sealed class DependenciesContainer
    {
        private Dictionary<Type, Dependency> dependencies;

        public Dependency Get<TDependency>()
        {
            return dependencies[typeof(TDependency)];
        }
        
        public Dependency Get(Type type)
        {
            return dependencies[type];
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
    }
}