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

using UnityEditor;
using UnityEngine;

namespace IndieInject.Editor
{
    [CustomEditor(typeof(SceneDependenciesRoot))]
    public class SceneDependenciesRootEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var root = (SceneDependenciesRoot)target;

            EditorGUI.BeginChangeCheck();

            var newEnableAutoInjector = (EnableAutoInjector)EditorGUILayout.EnumPopup("Enable Auto Injector", root.EnableAutoInjector);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(root, "Change Enable Auto Injector");
                root.EnableAutoInjector = newEnableAutoInjector;
                EditorUtility.SetDirty(root);

                if (root.EnableAutoInjector == EnableAutoInjector.Enable)
                {
                    if (root.GetComponent<SceneAutoInjector>() == null)
                    {
                        var existingInjectors = FindObjectsOfType<SceneAutoInjector>(true);

                        if (existingInjectors.Length == 0)
                        {
                            root.gameObject.AddComponent<SceneAutoInjector>();
                            Debug.Log("SceneAutoInjector added.");
                        }
                        else
                        {
                            Debug.LogWarning("SceneAutoInjector already exists in the scene. Cannot add another.");
                        }
                    }
                }
                else if (root.EnableAutoInjector == EnableAutoInjector.Disable)
                {
                    var injector = root.GetComponent<SceneAutoInjector>();
                    if (injector != null)
                    {
                        DestroyImmediate(injector);
                        Debug.Log("SceneAutoInjector removed.");
                    }
                }
            }
        }
    }
}