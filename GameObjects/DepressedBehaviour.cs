using MothDIed.ExtensionSystem;
using MothDIed.ServiceLocators;
using UnityEngine;

namespace MothDIed
{
    public abstract class DepressedBehaviour : MonoBehaviour
    {
        public readonly ServiceLocator<Component> CachedComponents = new ServiceLocator<Component>();
        public readonly ExtensionContainer ExtensionContainer = new ExtensionContainer();

        public virtual void WillBeDestroyed() { }
    }
}