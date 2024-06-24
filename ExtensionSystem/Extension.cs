using System;
using UnityEngine;

namespace destructive_code.ExtensionSystem
{
    public abstract class Extension : IDisposable
    {
        public DepressedBehaviour Owner { get; private set; }

        public void Initialize(DepressedBehaviour owner)
        {
            Owner = owner;
        }

        public virtual void StartExtension() {}
        public virtual void Dispose() {}
        
        public virtual void Enable() {}
        public virtual void Disable() {}
        
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        
    }
}