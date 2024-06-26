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
using System.Reflection;
using DIedMoth.Scenes;

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
        private DependenciesContainer sceneContainer;

        #region Registration
        public void RegisterGameDependencies(Startpoint startpoint)
        {
            var providers
                = startpoint.GetComponentsInChildren<IDependencyProvider>();

            Register(providers, gameContainer);
        }
        
        public void RegisterSceneDependencies(Scene scene)
        {
            sceneContainer = new DependenciesContainer();
            
            if (scene.Modules.TryGetModule(out SceneDependenciesModule dependenciesModule))
            {
                var providers = dependenciesModule.GetDependencies;
            
                Register(providers, sceneContainer);
            }
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

                    Type dependencyType = method.ReturnType;
                    
                    bool isSingleton = ((ProvideAttribute) Attribute.GetCustomAttribute(method, typeof(ProvideAttribute))).IsSingleton;

                    Func<object> fabric = (Func<object>)Delegate.CreateDelegate(typeof(Func<object>), null, method);
                    
                    var dependencyRegistration = new Dependency(dependencyType, fabric, isSingleton);

                    container.Add(dependencyType, dependencyRegistration);
                }
            }
        }

        #endregion

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

                        List<object> parameters = new List<object>();

                        foreach (var parameter in requiredParameters)
                        {
                            parameters.Add(Find(parameter.ParameterType).GetInstance());
                        }

                        info.Invoke(toInject, parameters.ToArray());
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
                        
                        info.SetValue(toInject, Find(info.FieldType).GetInstance());
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

                        info.SetValue(toInject, Find(info.PropertyType).GetInstance());
                    }
                }
            }
        }

        private Dependency Find(Type type)
        {
            if (gameContainer.Get(type) != null)
            {
                return gameContainer.Get(type);
            }
            if (sceneContainer.Get(type) != null)
            {
                return sceneContainer.Get(type);
            }

            throw new IndieResolveException($"There is no dependency of type {type}");
        }
    }
}