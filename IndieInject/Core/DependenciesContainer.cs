using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieInject
{
    public sealed class DependenciesContainer
    {
        private readonly Dictionary<Type, Dependency> dependencies = new ();

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
    }
}