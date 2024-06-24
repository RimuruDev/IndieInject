// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using destructive_code.Scenes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace IndieInject
{
    public class IndieInjector
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic;

        private readonly DependenciesContainer gameContainer = new();
        private readonly DependenciesContainer sceneContainer = new();

        public void RegisterGameDependencies(Startpoint startpoint)
        {
            var providers = startpoint.GetComponentsInChildren<IDependencyProvider>();

            Register(providers, gameContainer);
        }
        public void RegisterSceneDependencies(Scene scene)
        {
            var providers = scene.Dependencies;
            
            Register(providers, sceneContainer);
        }

        private void Register(IDependencyProvider[] providers, DependenciesContainer container)
        {
            foreach (var provider in providers)
            {
                var methods = provider.GetType().GetMethods(BindingFlags);
            
                foreach (var method in methods)
                {
                    if (!Attribute.IsDefined(method, typeof(ProvideAttribute)))
                    {
                        continue;
                    }

                    var returnType = method.ReturnType;
                    var fabric = method.Invoke(provider, parameters: null);

                    if (fabric != null)
                    {
                        var dependencyRegistration = new Dependency();
                        
                        dependencyRegistration.Fabric = fabric as Func<object>;
                        dependencyRegistration.IsSingleton = provider.IsSingleton;
                        
                        container.Add(returnType, dependencyRegistration);
                    }
                    else
                    {
                        throw new IndieProvideException($"<color=red>Provider {provider.GetType().Name} returned null for {returnType.Name}.</color>");
                    }
                }
            }
        }

        public void Inject(object toInject)
        {
            var type = toInject.GetType();

            //methods
            {
                var allMethods = type.GetMethods(BindingFlags);

                foreach (var info in allMethods)
                {
                    foreach (var attribute in info.GetCustomAttributes())
                    {
                        if (attribute is not InjectAttribute) continue;
                    
                        var requiredParameters = info.GetParameters();

                        info.Invoke(toInject, requiredParameters.Select(parameter => Find(parameter.ParameterType)).ToArray());
                    }
                }
            }
            
            //fields
            {
                var allFields = type.GetFields(BindingFlags);
        
                foreach (var info in allFields)
                {
                    foreach (var attribute in info.GetCustomAttributes())
                    {
                        if (attribute is not InjectAttribute) continue;
                        
                        info.SetValue(toInject, Find(info.FieldType));
                    }
                }
            }

            //properties
            {
                var allProperties = type.GetProperties(BindingFlags);
        
                foreach (var info in allProperties)
                {
                    foreach (var attribute in info.GetCustomAttributes())
                    {
                        if (attribute is not InjectAttribute) continue;

                        info.SetValue(toInject, Find(info.PropertyType));
                    }
                }
            }
        }

        private object Find(Type type)
        {
            if (gameContainer.Get(type) == null)
            {
                return gameContainer.Get(type);
            }
            else if (sceneContainer.Get(type) != null)
            {
                return sceneContainer.Get(type) != null;
            }

            return null;
        }
    }
}