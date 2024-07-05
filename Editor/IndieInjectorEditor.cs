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

using IndieInject;
using UnityEditor;
using UnityEngine;

namespace IndieInjectSample
{
    [HelpURL("https://github.com/RimuruDev/IndieInject")]
    public static class IndieInjectorEditor
    {
        private const string RootName = "[ === Dependencies Root === ]";
        private const string AutoInjectorName = "[ === Auto Injector === ]";

        [MenuItem("IndieInject/Setup Scene")]
        public static void SetupSceneRoot()
        {
            if (Object.FindObjectOfType<SceneDependenciesRoot>() == null)
            {
                var injectorObject = new GameObject(RootName);

                injectorObject.AddComponent<SceneDependenciesRoot>();

                Undo.RegisterCreatedObjectUndo(injectorObject, $"Create {RootName}");

                EditorUtility.SetDirty(injectorObject);
            }
            else
            {
                Debug.LogWarning($"An {RootName} already exists in the scene. Only one instance is allowed.");
            }
        }
        
        [MenuItem("IndieInject/Setup Scene Auto Inject")]
        public static void SetupSceneAutoInject()
        {
            if (Object.FindObjectOfType<SceneDependenciesRoot>())
            {
                var injectorObject = new GameObject(AutoInjectorName);

                injectorObject.AddComponent<SceneAutoInjector>();

                Undo.RegisterCreatedObjectUndo(injectorObject, $"Create {AutoInjectorName}");

                EditorUtility.SetDirty(injectorObject);
            }
            else
            {
                Debug.LogWarning("No dependencies root in scene");
            }
        }
        
        [MenuItem("IndieInject/Setup Entry Point")]
        public static void SetupCoreRoot()
        {
            if (Object.FindObjectOfType<SceneDependenciesRoot>() == null)
            {
                var rootObject = new GameObject(RootName);

                rootObject.AddComponent<CoreDependenciesRoot>();

                Undo.RegisterCreatedObjectUndo(rootObject, $"Create {RootName}");

                EditorUtility.SetDirty(rootObject);
            }
            else
            {
                Debug.LogWarning($"An {RootName} already exists in the scene. Only one instance is allowed.");
            }
        }

        [MenuItem("IndieInject/Clear")]
        public static void ClearIndieInjector()
        {
            {
                var coreRoot = Object.FindObjectOfType<CoreDependenciesRoot>();

                if (coreRoot != null)
                {
                    Undo.DestroyObjectImmediate(coreRoot.gameObject);
                    Debug.Log("Core Dependencies Root was deleted");
                }
            }
            {
                var sceneRoot = Object.FindObjectOfType<SceneDependenciesRoot>();

                if (sceneRoot != null)
                {
                    Undo.DestroyObjectImmediate(sceneRoot.gameObject);
                    Debug.Log("Scene Dependencies Root was deleted");
                }
            }
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
                EnsureSingleRoot();
            }
        }

        private static void EnsureSingleRoot()
        {
            {
                var roots = Object.FindObjectsOfType<CoreDependenciesRoot>();

                if (roots.Length > 1)
                {
                    Debug.LogWarning("More than one Core Root found. Keeping only the first one.");

                    for (var i = 1; i < roots.Length; i++)
                    {
                        Object.DestroyImmediate(roots[i].gameObject);
                    }
                }
            }
            {
                var roots = Object.FindObjectsOfType<SceneDependenciesRoot>();

                if (roots.Length > 1)
                {
                    Debug.LogWarning("More than one Scene Root found. Keeping only the first one.");

                    for (var i = 1; i < roots.Length; i++)
                    {
                        Object.DestroyImmediate(roots[i].gameObject);
                    }
                }
            }
        }
    }
}