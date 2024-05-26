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
using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace IndieInjector
{
    [DefaultExecutionOrder(-1000)]
    public class IndieInjector : MonoSingleton<IndieInjector>
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic;
        
        private readonly Dictionary<Type, object> dependencyRegistry = new();

        [SerializeField] private bool enableDebugLog = true;
        
        protected override void Awake()
        {
            base.Awake();

            RegisterAll();
            
            InjectAll();

            void RegisterAll()
            {
                var providers = GetAllIDependencyProvider();
            
                foreach (var provider in providers)
                {
                    RegisterProvider(provider);
                }
            }

            void InjectAll()
            {
                var injectables = FindMonoBehaviours().Where(IsInjectable);
            
                foreach (var injectable in injectables)
                {
                    Inject(injectable);
                }
            }
        }

        public void Inject(GameObject gameObjectForInject, InjectionType injectionType)
        {
            var components = gameObjectForInject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
            
            foreach (var component in components)
            {
                Inject(component, injectionType);
            }
        }

        public void RemoveDependencies(GameObject gameObjectForDespawn)
        {
            var components = gameObjectForDespawn.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
            
            foreach (var component in components)
            {
                Unregister(component);
            }
        }

        private void Unregister(object instanceForRemove)
        {
            var type = instanceForRemove.GetType();
            
            if (dependencyRegistry.ContainsKey(type))
            {
                dependencyRegistry.Remove(type);
                
                Log($"<color=yellow>Unregistered {type.Name}.</color>");
            }
        }

        [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Local")]
        private void Inject(object instanceForInject)
        {
            Inject(instanceForInject, InjectionType.All);
        }

        private void Inject(object instanceForInject, InjectionType injectionType)
        {
            var type = instanceForInject.GetType();

            if (injectionType.HasFlag(InjectionType.Fields))
            {
                var injectableFields = type
                    .GetFields(BindingFlags)
                    .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

                foreach (var injectableField in injectableFields)
                {
                    var fieldType = injectableField.FieldType;
                    var resolveInstance = Resolve(fieldType);

                    if (resolveInstance == null)
                    {
                        throw new IndieInjectException($"<color=red>Failed to resolve {fieldType.Name} for {type.Name}.</color>");
                    }

                    injectableField.SetValue(instanceForInject, resolveInstance);
                    
                    Log($"<color=magenta>Field Injected {fieldType.Name} into {type.Name}.</color>");
                }
            }

            if (injectionType.HasFlag(InjectionType.Properties))
            {
                var injectableProperties = type
                    .GetProperties(BindingFlags)
                    .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

                foreach (var injectableProp in injectableProperties)
                {
                    var propertyType = injectableProp.PropertyType;
                    var resolveInstance = Resolve(propertyType);

                    if (resolveInstance == null)
                    {
                        throw new IndieInjectException($"<color=red>Failed to resolve {propertyType.Name} for {type.Name}.</color>");
                    }

                    injectableProp.SetValue(instanceForInject, resolveInstance);
                    
                    Log($"<color=magenta>Property Injected {propertyType.Name} into {type.Name}.</color>");
                }
            }

            if (injectionType.HasFlag(InjectionType.Methods))
            {
                var injectableMethods = type
                    .GetMethods(BindingFlags)
                    .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

                foreach (var injectableMethod in injectableMethods)
                {
                    var requireParameters = injectableMethod
                        .GetParameters()
                        .Select(parameter => parameter.ParameterType)
                        .ToArray();
                    
                    var resolvedInstance = requireParameters.Select(Resolve).ToArray();

                    // ReSharper disable once VariableHidesOuterVariable
                    if (resolvedInstance.Any(resolvedInstance => resolvedInstance == null))
                    {
                        throw new IndieInjectException($"<color=red>Failed to inject {type.Name}.{injectableMethod.Name}.</color>");
                    }

                    injectableMethod.Invoke(instanceForInject, resolvedInstance);
                    
                    Log($"<color=magenta>Method Injected {type.Name}.{injectableMethod.Name}.</color>");
                }
            }
        }

        private object Resolve(Type type)
        {
            dependencyRegistry.TryGetValue(type, out var resolvedInstance);
            
            return resolvedInstance;
        }

        private static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(BindingFlags);
            
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        private void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(BindingFlags);
            
            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute)))
                {
                    continue;
                }

                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, parameters: null);

                if (providedInstance != null)
                {
                    dependencyRegistry[returnType] = providedInstance;
                    
                    Log($"<color=yellow>Registered {returnType.Name} from {provider.GetType().Name}.</color>");
                }
                else
                {
                    throw new IndieProvideException($"<color=red>Provider {provider.GetType().Name} returned null for {returnType.Name}.</color>");
                }
            }
        }

        private static IEnumerable<MonoBehaviour> FindMonoBehaviours()
        {
            const FindObjectsSortMode sortedMode = FindObjectsSortMode.InstanceID;
            
            return FindObjectsByType<MonoBehaviour>(sortedMode);
        }

        private static IEnumerable<IDependencyProvider> GetAllIDependencyProvider() =>
            FindMonoBehaviours().OfType<IDependencyProvider>();

        private void Log(string message)
        {
            if(!enableDebugLog)
                return;

            Debug.Log(message, gameObject);
        }
    }
}