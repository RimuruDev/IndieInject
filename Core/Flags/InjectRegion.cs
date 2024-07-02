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

namespace IndieInject
{
    [Flags]
    public enum InjectRegion
    {
        Fields = 1,
        Properties = 2,
        Methods = 4,
        All = Fields | Properties | Methods
    }
}