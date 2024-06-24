using UnityEngine;

namespace destructive_code.LevelGeneration.GUIWindows
{
    public sealed class SceneGUI
    {
        public GUILayer CurrentLayer { get; private set; }
        public GUILayer PreviousLayer { get; private set; }
        
        public void OpenLayer(GUILayer layer)
        {
            PreviousLayer = CurrentLayer;
            
            if(CurrentLayer != null) 
                CurrentLayer.CloseLayer();
            
            layer.OpenLayer();
            
            CurrentLayer = layer;
        }

        public void CloseCurrent()
        {
            if(CurrentLayer == null)
                return;
            
            PreviousLayer = CurrentLayer;
            
            CurrentLayer.CloseLayer();
            
            CurrentLayer = null;
        }

        public void BackToPrevious()
        {
            OpenLayer(PreviousLayer);
        }
    }
}