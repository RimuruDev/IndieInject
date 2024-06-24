using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace destructive_code.SceneLoader
{
    public sealed class AsyncSceneLoader
    {
        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null)
        {
            LoadSceneCoroutine(sceneName, onSceneLoadedCallback);
        }

        private async void LoadSceneCoroutine(string sceneName, Action onSceneLoadedCallback = null)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
                return;
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            while (!loadSceneOperation.isDone)
                await UniTask.WaitForFixedUpdate();
            
            onSceneLoadedCallback?.Invoke();
        }

        public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}