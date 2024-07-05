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
    public sealed class Dependency : IDisposable
    {
        public readonly Type Type;
        public readonly bool IsSingleton;
        
        private readonly Func<object> fabric;
        private object instance;

        public Dependency(Type type, Func<object> fabric, bool isSingleton)
        {
            Type = type;
            this.fabric = fabric;
            IsSingleton = isSingleton;
        }

        public void Dispose()
        {
            if (IsSingleton && instance is IDisposable disposable)
            {
                disposable.Dispose();   
            }
        }

        public object GetInstance()
        {
            if (IsSingleton && instance == null)
            {
                instance = fabric.Invoke();
            }
            else if (IsSingleton)
            {
                if (instance == null)
                {
                    throw new IndieProvideException($"Your provider of type {Type} returns null");
                }
                
                return instance;
            }

            object newInstance = fabric.Invoke();
            
            if(newInstance == null)
            {
                throw new IndieProvideException($"Your provider of {Type} provides null");
            }

            return fabric.Invoke();
        }
    }
}