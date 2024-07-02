// **************************************************************** //
//
//   Copyright (c) YUJECK. All rights reserved.
//   Contact me: 
//          - GitHub:   https://github.com/YUJECK
//
// **************************************************************** //

using UnityEngine;

namespace IndieInject
{
    public sealed class SceneAutoInjector : MonoBehaviour
    {
        private void Start()
        {
            var all = FindObjectsOfType<MonoBehaviour>(true);

            foreach (var monoBehaviour in all)
            {
                Indie.Injector.Inject(monoBehaviour);
            }
        }
    }
}