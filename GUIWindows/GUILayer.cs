using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace destructive_code.LevelGeneration.GUIWindows
{
    public abstract class GUILayer : MonoBehaviour
    {
        public bool LayerOpened { get; private set; }
        
        public abstract bool DisablePlayerControls { get; }
        
        private readonly List<GUIWindow> all = new List<GUIWindow>();
        private readonly List<GUIWindow> current = new List<GUIWindow>();

        protected virtual void OnLayerOpened() {}
        protected virtual void OnLayerClosed() {}
        
        private void Start()
        {
            var windows = GetComponentsInChildren<GUIWindow>(true);

            foreach (var window in windows)
            {
                AddWindow(window);
            }
            
            CloseLayer();   
        }

        public void OpenLayer()
        {
            LayerOpened = true;
            
            foreach (var window in all)
            {
                OpenWindow(window);
            }
            
            OnLayerOpened();
        }

        public void CloseLayer()
        {
            foreach (var window in current)
            {
                CloseWindowBase(window);
            }
            
            LayerOpened = false;
            OnLayerClosed();
        }

        public void AddWindow(GUIWindow window)
        {
            all.Add(window);    
            current.Add(window);
        }

        public void RemoveWindow(GUIWindow window)
        {
            all.Remove(window);
            current.Remove(window);
        }

        public void OpenWindow(GUIWindow window)
        {
            if (!LayerOpened)
                return;
            
            if(!all.Contains(window))
                AddWindow(window);
            
            window.gameObject.SetActive(true);
            
            Type guiWindowType = typeof(GUIWindow);
            
            guiWindowType
                .GetMethod("OnThisOpened", BindingFlags.Instance | BindingFlags.NonPublic)?
                .Invoke(window, new object[] { this });

            var onOpenedCallback = guiWindowType.GetMethod("OnOtherOpened", BindingFlags.Instance | BindingFlags.NonPublic);
            
            foreach (var other in current)
            {
                onOpenedCallback.Invoke(other, new object[] { this, window });
            }
            
            current.Add(window);
        }

        private void CloseWindowBase(GUIWindow window)
        {   
            Type guiWindowType = typeof(GUIWindow);
            
            window.gameObject.SetActive(false);
            
            guiWindowType
                .GetMethod("OnThisClosed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(window, new object[] { this });
        }
        
        public void CloseWindow(GUIWindow window)
        {
            if (!LayerOpened)
                return;
            
            CloseWindowBase(window);

            current.Remove(window);
        }

        public void CloseAllExcept(params GUIWindow[] windows)
        {
            if (!LayerOpened)
                return;
            
            foreach (var window in current)
            {
                if(!windows.Contains(window))
                    CloseWindow(window);
            }
        }
    }
}