using destructive_code.LevelGeneration;
using destructive_code.ExtensionSystem;
using destructive_code.ServiceLocators;
using UnityEngine;

namespace destructive_code
{
    public class DepressedBehaviour : MonoBehaviour
    {
        public readonly LocalServiceLocator<Component> CachedComponents = new LocalServiceLocator<Component>();
        public readonly ExtensionContainer ExtensionContainer = new ExtensionContainer();

        public virtual void WillBeDestroyed() { }
    }
}