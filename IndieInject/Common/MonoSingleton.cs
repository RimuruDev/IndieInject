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
    public class MonoSingleton<TComponent> : MonoBehaviour where TComponent : Component
    {
        protected static TComponent instance;

        public static bool HasInstance => instance != null;

        public static TComponent TryGetInstance() => HasInstance ? instance : null;

        public static TComponent Current => instance;

        public static TComponent Self => instance;

        public static TComponent Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                instance = FindFirstObjectByType<TComponent>();

                if (instance != null)
                    return instance;

                var obj = new GameObject
                {
                    name = $"[ === {typeof(TComponent).Name} === ]"
                };

                instance = obj.AddComponent<TComponent>();

                return instance;
            }
        }

        protected virtual void Awake() =>
            InitializeSingleton();

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
                return;

            instance = this as TComponent;
        }
    }
}