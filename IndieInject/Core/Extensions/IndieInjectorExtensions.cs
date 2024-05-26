// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using UnityEngine;

namespace IndieInjector
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public static class IndieInjectorExtensions
    {
        public static GameObject InstantiateWithDependencies(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(prefab, position, rotation);
            IndieInjector.Instance.Inject(instance, InjectionType.All);
            return instance;
        }

        public static GameObject InstantiateWithFields(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(prefab, position, rotation);
            IndieInjector.Instance.Inject(instance, InjectionType.Fields);
            return instance;
        }

        public static GameObject InstantiateWithProperties(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(prefab, position, rotation);
            IndieInjector.Instance.Inject(instance, InjectionType.Properties);
            return instance;
        }

        public static GameObject InstantiateWithMethods(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(prefab, position, rotation);
            IndieInjector.Instance.Inject(instance, InjectionType.Methods);
            return instance;
        }

        public static GameObject InstantiateWithDependencies(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            IndieInjector.Instance.Inject(instance, InjectionType.All);
            return instance;
        }

        public static GameObject InstantiateWithFields(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            IndieInjector.Instance.Inject(instance, InjectionType.Fields);
            return instance;
        }

        public static GameObject InstantiateWithProperties(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            IndieInjector.Instance.Inject(instance, InjectionType.Properties);
            return instance;
        }

        public static GameObject InstantiateWithMethods(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            IndieInjector.Instance.Inject(instance, InjectionType.Methods);
            return instance;
        }

        public static void DestroyWithDependencies(GameObject gameObject, float timeToDestroy = 0f)
        {
            IndieInjector.Instance.RemoveDependencies(gameObject);
            Object.Destroy(gameObject, timeToDestroy);
        }
    }
}