using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tatedrez
{
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (Application.isMobilePlatform && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Application.targetFrameRate = 60;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            if (scene.buildIndex == Scene.Initial)
                SceneManager.LoadScene(Scene.Main);
        }
    }
}
