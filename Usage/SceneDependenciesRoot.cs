// **************************************************************** //
//
//   Copyright (c) RimuruDev, YUJECK. All rights reserved.
//   Contact us:
//          - RimuruDev: 
//              - Gmail:    rimuru.dev@gmail.com
//              - LinkedIn: https://www.linkedin.com/in/rimuru/
//              - GitHub:   https://github.com/RimuruDev
//          - YUJECK:
//              - GitHub:   https://github.com/YUJECK
//
//   This project is licensed under the MIT License.
//   See the LICENSE file in the project root for more information.
//
// **************************************************************** //

using UnityEngine;

namespace IndieInject
{
    public enum EnableAutoInjector : byte
    {
        Enable = 0,
        Disable = 1,
    }

    [DefaultExecutionOrder(-500)]
    public sealed class SceneDependenciesRoot : MonoBehaviour
    {
#if UNITY_EDITOR
        public EnableAutoInjector EnableAutoInjector;
#endif

        private void Awake()
        {
            Indie.Injector.RegisterSceneDependencies(this);
        }
    }
}