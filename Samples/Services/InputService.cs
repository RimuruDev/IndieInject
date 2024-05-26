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
    public class InputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public static Vector3 GetInput()
        {
            var horizontal = Input.GetAxis(Horizontal);
            var vertical = Input.GetAxis(Vertical);

            return new Vector3(horizontal, 0, vertical);
        }
    }
}