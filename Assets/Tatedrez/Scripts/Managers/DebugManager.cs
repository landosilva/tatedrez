using Lando.Plugins.Singletons.ScriptableObject;
using UnityEngine;

namespace Tatedrez.Managers
{
    [CreateAssetMenu(menuName = "Tatedrez/Managers/DebugManager", fileName = "DebugManager")]
    public class DebugManager : Singleton<DebugManager>
    {
        [SerializeField] private bool _enabled = true;
        
        public static void Enable() => Instance._enabled = true;
        public static void Disable() => Instance._enabled = false;
        
        public static void Log(object message)
        {
            if (!Instance._enabled) 
                return;
            
            Debug.Log(message);
        }
    }
}