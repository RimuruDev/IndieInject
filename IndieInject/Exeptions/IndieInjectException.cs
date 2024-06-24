// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using System;
using UnityEngine;

namespace IndieInject
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public sealed class IndieInjectException : Exception
    {
        public IndieInjectException() : base()
        {
        }

        public IndieInjectException(string message) : base(message)
        {
        }

        public IndieInjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}