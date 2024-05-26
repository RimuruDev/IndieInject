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
    [RequireComponent(typeof(Rigidbody))]
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public class Hero : MonoBehaviour
    {
        [Inject] private InputService inputService;
        [Inject] private HeroConfig HeroConfig;

        private Rigidbody heroRigidbody;

        private void Awake() =>
            heroRigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            var input = InputService.GetInput();

            var movement = input * (HeroConfig.MoveSpeed * Time.deltaTime);

            heroRigidbody.MovePosition(transform.position + movement);
        }
    }
}