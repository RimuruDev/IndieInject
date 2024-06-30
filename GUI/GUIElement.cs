using System.Collections.Generic;
using UnityEngine;

namespace MothDIed.GUI
{
    public abstract class GUIElement : MonoBehaviour
    {
        public GUIElement[] Children => children.ToArray();
        private readonly List<GUIElement> children = new();
        
        public GUIElement Parent { get; private set; }
        
        public bool Active => gameObject.activeSelf;

        public virtual bool MoveOnUIInputMode { get; } = false;

        private void Awake()
        {
            InitChildren();
        }

        private void InitChildren()
        {
            children.Clear();
            var allElements = transform.GetComponentsInChildren<GUIElement>();

            foreach (var element in allElements)
            {
                if (element.Parent == this)
                {
                    children.Add(element);
                }
            }
        }

        protected virtual void OnShown() { }
        protected virtual void OnHidden() { }

        public virtual void MoveUnder(GUIElement guiElement)
        {
            transform.parent = guiElement.transform;
            Parent = guiElement;
        }
        
        public virtual void OnOtherShown(GUIElement guiElement) { }
        public virtual void OnOtherHidden(GUIElement guiElement) { }

        public TElement Get<TElement>()
            where TElement : GUIElement
        {
            foreach (var element in children)
            {
                if (element is TElement guiElement)
                {
                    return guiElement;
                }
            }

            return null;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            OnShown();
            
            if(Parent != null)
            {
                Parent.OnChildShown(this);
            }
        }

        public void Hide()
        {
            OnHidden();

            gameObject.SetActive(false);
            
            if(Parent != null)
            {
                Parent.OnChildHidden(this);
            }
        }

        public void OnChildHidden(GUIElement element)
        {
            foreach (var child in children)
            {
                child.OnOtherHidden(child);
            }
        }

        public void OnChildShown(GUIElement element)
        {
            foreach (var child in children)
            {
                child.OnOtherShown(child);
            }
        }
    }
}