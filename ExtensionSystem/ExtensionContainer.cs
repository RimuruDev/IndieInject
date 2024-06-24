using System;
using System.Collections.Generic;
using UnityEngine;

namespace destructive_code.ExtensionSystem
{
    public sealed class ExtensionContainer
    {
        private DepressedBehaviour owner;

        private readonly Dictionary<Type, List<Extension>> extensions = new();

        private bool containerStarted;

        ~ExtensionContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.Dispose());
            }
        }
        
        public void StartContainer(DepressedBehaviour owner, params Extension[] extensions)
        {
            this.owner = owner;

            foreach (Extension extension in extensions)
            {
                AddExtension(extension);
            }

            containerStarted = true;

            foreach (var extension in this.extensions)
            {
                extension.Value.ForEach((extension) => extension.StartExtension());
            }
        }

        public TExtension GetExtension<TExtension>()
            where TExtension : Extension
        {
            return extensions[typeof(TExtension)][0] as TExtension;
        }

        public TExtension[] GetExtensions<TExtension>()
            where TExtension : Extension
        {
            return extensions[typeof(TExtension)].ToArray() as TExtension[];
        }
        
        public bool HasExtension<TExtension>()
            where TExtension : Extension
        {
            return extensions.ContainsKey(typeof(TExtension));
        }
        
        public void AddExtension(Extension extension)
        {
            extension.Initialize(owner);

            if (Attribute.GetCustomAttribute(extension.GetType(), typeof(DisallowMultipleExtensionsAttribute)) != null)
            {
                if (extensions.ContainsKey(extension.GetType()))
                {
#if UNITY_EDITOR
                    Debug.Log("YOU TRIED TO ADD MULTIPLE EXTENSIONS OF TYPE " + extension.GetType());
#endif
                    return;
                }
            }

            extensions.TryAdd(extension.GetType(), new List<Extension>());
            extensions[extension.GetType()].Add(extension);

            if (containerStarted)
            {
                extension.Enable();
                extension.StartExtension();
            }
                
        }

        public void RemoveExtension<TExtension>()
            where TExtension : Extension
        {
            Type extension = typeof(TExtension);

            if (extensions.ContainsKey(extension))
            {
                extensions[extension][0].Dispose();
                extensions[extension].RemoveAt(0);

                if (extensions[extension].Count == 0)
                    extensions.Remove(extension);
            }
        }

        public void RemoveAllExtensions<TExtension>()
            where TExtension : Extension
        {
            while (extensions.ContainsKey(typeof(Extension)))
            {
                RemoveExtension<TExtension>();
            }
        }
        
        public void RemoveExtensions<TExtension>(int count)
            where TExtension : Extension
        {
            for(int i = 0; i < count; i++)
            {
                RemoveExtension<TExtension>();
            }
        }

        
        //callbacks from owner
        public void EnableContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.Enable());
            }
        }
        public void DisableContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.Disable());
            }
        }
        public void UpdateContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.Update());
            }
        }
        public void FixedUpdateContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.FixedUpdate());
            }
        }
    }
}