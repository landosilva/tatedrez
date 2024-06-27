using System.Collections.Generic;
using System.Linq;
using Lando.Core;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lando.Plugins.Singletons.ScriptableObject
{
    public abstract class Singleton : UnityEngine.ScriptableObject
    {
        protected virtual void OnEnable() => OnLoad();

        public abstract void OnLoad();
    }
    
    public abstract class Singleton<T> : Singleton where T : Singleton<T>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBeProtected.Global
        public static T Instance { get; private set; }

        public override void OnLoad()
        {
#if UNITY_EDITOR
            PreloadedAssets.Add(this);
#endif
            Instance = (T) this;
        }
    }

#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class SingletonLoader
    {
        static SingletonLoader()
        {
            List<Object> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            foreach (Singleton singleton in preloadedAssets.OfType<Singleton>()) 
                singleton.OnLoad();
        }
    }
#endif
}
