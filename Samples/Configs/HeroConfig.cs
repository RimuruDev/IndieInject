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
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Config/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 50f;
    }
}