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
    public sealed class CoreDependenciesRoot : MonoBehaviour
    {
        private void Awake()
        {
            Indie.Injector.RegisterDependenciesToCore(this);
        }
    }
}