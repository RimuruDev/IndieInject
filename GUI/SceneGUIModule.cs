using MothDIed.Scenes;
using MothDIed.Scenes.SceneModules;
using UnityEngine;

namespace MothDIed.GUI
{
    public sealed class SceneGUIModule : SceneModule
    {
        public GUILayer CurrentLayer { get; private set; }
        public GUILayer PreviousLayer { get; private set; }

        private bool findStartLayerOnStart;

        public SceneGUIModule(bool findStartLayerOnStart)
        {
            this.findStartLayerOnStart = findStartLayerOnStart;
        }

        public override void StartModule(Scene scene)
        {
            if(!findStartLayerOnStart) return;
            
            var layers = GameObject.FindObjectsOfType<GUILayer>();
            var startLayer = GameObject.FindObjectOfType<StartLayer>();
                
            foreach (var layer in layers)
            {
                if(layer.gameObject != startLayer.gameObject)
                    layer.Hide();   
            }
            
            OpenLayer(startLayer.GetComponent<GUILayer>());
        }

        public void OpenLayer(GUILayer layer)
        {
            PreviousLayer = CurrentLayer;
            
            if(CurrentLayer != null) 
                CurrentLayer.Hide();
            
            layer.Show();
            
            CurrentLayer = layer;
        }

        public void CloseCurrent()
        {
            if(CurrentLayer == null)
                return;
            
            PreviousLayer = CurrentLayer;
            
            CurrentLayer.Hide();
            
            CurrentLayer = null;
        }

        public void BackToPrevious()
        {
            OpenLayer(PreviousLayer);
        }
    }
}