// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using UnityEditor;
using UnityEngine;

namespace IndieInjector
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public static class IndieInjectorEditor
    {
        private const string InjectorName = "[ === IndieInjector === ]";

        [MenuItem("IndieInjector/Setup")]
        public static void SetupIndieInjector()
        {
            if (Object.FindObjectOfType<IndieInjector>() == null)
            {
                var injectorObject = new GameObject(InjectorName);

                injectorObject.AddComponent<IndieInjector>();

                Undo.RegisterCreatedObjectUndo(injectorObject, "Create IndieInjector");

                EditorUtility.SetDirty(injectorObject);
            }
            else
            {
                Debug.LogWarning("An IndieInjector already exists in the scene. Only one instance is allowed.");
            }
        }

        [MenuItem("IndieInjector/Clear")]
        public static void ClearIndieInjector()
        {
            var injector = Object.FindObjectOfType<IndieInjector>();

            if (injector != null)
                Undo.DestroyObjectImmediate(injector.gameObject);
            else
                Debug.LogWarning("No IndieInjector found in the scene to clear.");
        }

        [InitializeOnLoadMethod]
        private static void OnProjectLoadedInEditor()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                EnsureSingleIndieInjector();
            }
        }

        private static void EnsureSingleIndieInjector()
        {
            var injectors = Object.FindObjectsOfType<IndieInjector>();

            if (injectors.Length > 1)
            {
                Debug.LogWarning("More than one IndieInjector found. Keeping only the first one.");

                for (var i = 1; i < injectors.Length; i++)
                {
                    Object.DestroyImmediate(injectors[i].gameObject);
                }
            }
        }
    }
}