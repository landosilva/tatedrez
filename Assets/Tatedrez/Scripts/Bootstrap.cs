using UnityEngine;

namespace Tatedrez
{
    public class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            Application.targetFrameRate = 300;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
