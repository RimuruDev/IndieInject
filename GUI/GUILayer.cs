using UnityEngine;

namespace MothDIed.GUI
{
    public abstract class GUILayer : GUIElement
    {
        public override void MoveUnder(GUIElement guiElement)
        {
#if UNITY_EDITOR
            Debug.LogError($"You tried to set parent to GUILayer. Parent -> {guiElement.name}; Layer -> {this.name}");
#endif
        }
    }
}