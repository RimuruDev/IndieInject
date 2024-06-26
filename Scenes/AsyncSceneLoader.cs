using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DIedMoth.SceneLoader
{
    public sealed class AsyncSceneLoader
    {
        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null)
        {
            LoadSceneProcess(sceneName, onSceneLoadedCallback);
        }

        private async void LoadSceneProcess(string sceneName, Action onSceneLoadedCallback = null)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
                return;
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            loadSceneOperation.allowSceneActivation = false;

            // loadSceneOperation.completed += Completed;
            
            while (!loadSceneOperation.isDone)
            {
                if (loadSceneOperation.progress >= 0.9f)
                {
                    onSceneLoadedCallback?.Invoke();

                    Debug.Log(GameObject.FindObjectsOfType<MonoBehaviour>().Length);
                    
                    loadSceneOperation.allowSceneActivation = true;
                }
                
                await UniTask.Yield();
            }
            
            //onSceneLoadedCallback?.Invoke();
            
            void Completed(AsyncOperation obj)
            {
                onSceneLoadedCallback?.Invoke();
            }
        }


        public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;

        class LoadProcess
        {
            public string ToScene;
            public string BeforeLoaded;
            public string OnLoaded;
        }
    }
}