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
        public List<Terrain> ProvidePersistentProgressService()
        {
            return new List<Terrain>();
        }
        
        [Provide(true)]
        public List<string> Test()
        {
            return new List<string>();
        }
    }
}