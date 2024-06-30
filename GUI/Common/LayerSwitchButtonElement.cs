using System;
using MothDIed.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace MothDIed.GUI
{
    [RequireComponent(typeof(Button))]
    public class LayerSwitchButtonElement : GUIElement
    {
        [SerializeField] private GUILayer toLayer;

        private SceneGUIModule sceneGUIModule;
        
        private void Start()
        {
            InitSceneGUI();
            GetComponent<Button>().onClick.AddListener(Switch);
        }

        private void InitSceneGUI()
        {
            sceneGUIModule = Game.CurrentScene.Modules.Get<SceneGUIModule>();

            if (sceneGUIModule == null)
            {
                throw new NullReferenceException(nameof(sceneGUIModule));
            }
        }
        
        private void Switch()
        {
            sceneGUIModule.OpenLayer(toLayer);
        }
    }
}