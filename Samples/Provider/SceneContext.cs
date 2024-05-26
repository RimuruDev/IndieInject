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
    [SelectionBase]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public sealed class SceneContext : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform rootTransformForHero;
        [SerializeField] private HeroConfig heroConfig;

        [Provide]
        public GameObject ProvidePlayerPrefab() => playerPrefab;

        [Provide]
        public InputService ProvideInputService() => new();

        [Provide]
        public HeroConfig ProvidePlayerConfig() => heroConfig;

        [Provide]
        public Transform RootTransformForHero() => rootTransformForHero;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            // Just in case someone loses their links when they download the example, an attempt will be made to restore them. //

            if (playerPrefab == null)
                playerPrefab = Resources.Load<GameObject>(AssetPath.HeroPrefab);

            if (heroConfig == null)
                heroConfig = Resources.Load<HeroConfig>(AssetPath.HeroConfig);

            if (rootTransformForHero != null)
                return;

            rootTransformForHero = GameObject.Find(Const.ParentForRuntimeObj)?.transform;

            if (rootTransformForHero == null)
                rootTransformForHero = new GameObject(Const.ParentForRuntimeObj).transform;
        }
    }
}