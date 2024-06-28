// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace IndieInject
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public class MonoProvider : MonoBehaviour, IDependencyProvider
    {
        [Provide(true)]
        public MonoBehaviour ProvidePersistentProgressService()
        {
            return FindObjectOfType<MonoBehaviour>();
        }
        
        [Provide(true)]
        public NewBehaviourScript1.ColorT ColorTest()
        {
            return new NewBehaviourScript1.ColorT (Color.cyan);
        }
        
        [Provide(true)]
        public List<string> Test()
        {
            return new List<string>();
        }
    }
}