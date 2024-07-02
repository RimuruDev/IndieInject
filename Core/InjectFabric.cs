// **************************************************************** //
//
//   Copyright (c) YUJECK. All rights reserved.
//   Contact me: 
//          - GitHub:   https://github.com/YUJECK
//
// **************************************************************** //

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IndieInject
{
    public sealed class InjectFabric
    {
        public event System.Action<Object> OnInstantiated;
        public event System.Action<Object> OnDestroyed;
        
        public event Action<GameObject> OnGameObjectInstantiated;
        public event Action<GameObject> OnGameObjectDestroyed;

        public TObject Instantiate<TObject>(TObject original, Vector3 position)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, null);
        }

        public TObject Instantiate<TObject>(TObject original, Transform transform)
            where TObject : Object
        {
            return Instantiate(original, transform.position, Quaternion.identity, transform);
        }

        
        public TObject Instantiate<TObject>(TObject original, Vector3 position, Transform parent)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, parent);
        }

        public TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
            where TObject : MonoBehaviour
        {
            return Instantiate(original, position, rotation, null);
        }

        public TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation,
            Transform parent)
            where TObject : Object
        {
            var instance = Object.Instantiate(original, position, rotation, parent);
            
            OnInstantiated?.Invoke(instance);
            InvokeGameObjectEventsIfGameObject();

            Indie.Injector.Inject(instance);
            
            return instance;

            void InvokeGameObjectEventsIfGameObject()
            {
                if (instance is GameObject gameObject)
                {
                    OnGameObjectInstantiated?.Invoke(gameObject);
                }
                else if (instance is Component component)
                {
                    OnGameObjectInstantiated?.Invoke(component.gameObject);
                }
            }
        }

        public void Destroy(Object toDestroy)
        {
            OnDestroyed?.Invoke(toDestroy);
            
            if(toDestroy is GameObject gameObject)
            {
                OnGameObjectDestroyed?.Invoke(gameObject);
            }
            
            Object.Destroy(toDestroy);
        }
    }
}