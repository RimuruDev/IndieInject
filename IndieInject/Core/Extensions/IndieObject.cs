// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

// TODO: Add Generic variant's

using UnityEngine;

namespace IndieInjector
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public static class IndieObject
    {
        public static GameObject InstantiateWithDependencies(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return IndieInjectorExtensions.InstantiateWithDependencies(prefab, position, rotation);
        }

        public static GameObject InstantiateWithFields(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return IndieInjectorExtensions.InstantiateWithFields(prefab, position, rotation);
        }

        public static GameObject InstantiateWithProperties(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return IndieInjectorExtensions.InstantiateWithProperties(prefab, position, rotation);
        }

        public static GameObject InstantiateWithMethods(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return IndieInjectorExtensions.InstantiateWithMethods(prefab, position, rotation);
        }

        public static GameObject InstantiateWithDependencies(GameObject prefab, Transform parent)
        {
            return IndieInjectorExtensions.InstantiateWithDependencies(prefab, parent);
        }

        public static GameObject InstantiateWithFields(GameObject prefab, Transform parent)
        {
            return IndieInjectorExtensions.InstantiateWithFields(prefab, parent);
        }

        public static GameObject InstantiateWithProperties(GameObject prefab, Transform parent)
        {
            return IndieInjectorExtensions.InstantiateWithProperties(prefab, parent);
        }

        public static GameObject InstantiateWithMethods(GameObject prefab, Transform parent)
        {
            return IndieInjectorExtensions.InstantiateWithMethods(prefab, parent);
        }

        public static void DestroyWithDependencies(GameObject gameObject, float timeToDestroy = 0f)
        {
            IndieInjectorExtensions.DestroyWithDependencies(gameObject, timeToDestroy);
        }
    }
}