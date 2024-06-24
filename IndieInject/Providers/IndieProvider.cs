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

namespace IndieInject
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public class IndieProvider : MonoBehaviour, IDependencyProvider
    {
        // =================== //
        // NOTE: Resolve this! //
        // =================== //
        // [Provide]
        // public PersistentProgressService ProvidePersistentProgressService()
        // {
        //     Debug.Log("Providing PersistentProgressService");
        //     return new PersistentProgressService();
        // }
        public bool IsSingleton { get; }
    }
}