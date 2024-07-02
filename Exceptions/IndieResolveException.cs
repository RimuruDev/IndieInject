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
    public sealed class IndieResolveException : Exception
    {
        public IndieResolveException() : base()
        {
        }

        public IndieResolveException(string message) : base(message)
        {
        }

        public IndieResolveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}