using System;
using MothDIed.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MothDIed.Scenes
{
    public class GameFabric
    {
        public event Action<Object> OnInstantiated;
        public event Action<Object> OnDestroyed;
        public event Action<GameObject> OnGameObjectInstantiated;
        public event Action<GameObject> OnGameObjectDestroyed;
        
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, null);
        }
        
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Transform parent)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, parent);
        }
        
        public virtual  TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
            where TObject : MonoBehaviour
        {
            return Instantiate(original, position, rotation, null);
        }

        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation,
            Transform parent)
            where TObject : Object
        {
            var instance = GameObject.Instantiate(original, position, rotation, parent);

            OnInstantiated?.Invoke(instance);

            if (instance is GameObject gameObject)
            {
                OnGameObjectInstantiated?.Invoke(gameObject);
            }
            else if (instance is Component component)
            {
                OnGameObjectInstantiated?.Invoke(component.gameObject);
            }

            Game.Injector.Inject(instance);
            
            return instance;
        }

        public virtual async void Destroy(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out DestroyDelay destroyDelay))
            {
                var behaviours = gameObject.GetComponents<DepressedBehaviour>();
                
                for (int i = 0; i < behaviours.Length; i++)
                {
                    behaviours[i].WillBeDestroyed();
                }
                
                await destroyDelay.Destroy();
            }

            OnDestroyed?.Invoke(gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}