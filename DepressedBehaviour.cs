using DIedMoth.LevelGeneration;
using DIedMoth.ExtensionSystem;
using DIedMoth.ServiceLocators;
using UnityEngine;

namespace DIedMoth
{
    public class DepressedBehaviour : MonoBehaviour
    {
        public readonly ServiceLocator<Component> CachedComponents = new ServiceLocator<Component>();
        public readonly ExtensionContainer ExtensionContainer = new ExtensionContainer();

        public virtual void WillBeDestroyed() { }
    }
}