using System;
using UnityEngine.SceneManagement;

namespace DIedMoth.SceneLoader
{
    public sealed class AsyncSceneLoader
    {
        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            loadSceneOperation.completed += (_) => onSceneLoadedCallback?.Invoke();
        }

        private string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}