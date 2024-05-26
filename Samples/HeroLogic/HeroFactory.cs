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
    public class HeroFactory : MonoBehaviour
    {
        // === Inject in field === //
        [Inject] private GameObject playerPrefab;
        private Transform rootTransform;

        // === Inject in method === //
        [Inject]
        private void Constructor(Transform rootTransformForSpawnHero)
        {
            rootTransform = rootTransformForSpawnHero;
        }

        private void Start()
        {
            var hero = IndieObject.InstantiateWithDependencies(playerPrefab, rootTransform);
        }
    }
}