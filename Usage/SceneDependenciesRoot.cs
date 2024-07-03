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
    public sealed class SceneDependenciesRoot : MonoBehaviour
    {
        private void Awake()
        {
            Indie.Injector.RegisterSceneDependencies(this);
        }
    }
}