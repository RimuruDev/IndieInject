// **************************************************************** //
//
//   Copyright (c) RimuruDev, YUJECK. All rights reserved.
//   Contact us:
//          - RimuruDev: 
//              - Gmail:    rimuru.dev@gmail.com
//              - LinkedIn: https://www.linkedin.com/in/rimuru/
//              - GitHub:   https://github.com/RimuruDev
//          - YUJECK:
//              - GitHub:   https://github.com/YUJECK
//
//   This project is licensed under the MIT License.
//   See the LICENSE file in the project root for more information.
//
// **************************************************************** //

using System;

namespace IndieInject
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectRegionAttribute : Attribute
    {
        public InjectRegion Region;

        public InjectRegionAttribute(InjectRegion region)
        {
            Region = region;
        }
    }
}