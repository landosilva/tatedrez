// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

using UnityEngine;

namespace Lando.Plugins.Singletons.MonoBehaviour
{
    public interface ISingleton
    {
        bool Initialized { get; }
    }
    
    public abstract class Singleton<T> : UnityEngine.MonoBehaviour, ISingleton where T : Singleton<T>
    {
        protected static T _instance;
        public static T Instance => _instance;

        public bool Initialized => Instance != null;
        
        protected virtual void Awake()
        {
            if (Initialized)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = (T) this;
        }

        protected virtual void OnDestroy() { }
    }
    
    public abstract class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>
    {
        public new static T Instance => GetOrCreateInstance();

        protected override void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
        
        protected static T GetOrCreateInstance()
        {
            if (_instance != null) 
                return _instance;
            
            _instance = FindAnyObjectByType<T>();
            if (_instance != null) 
                return _instance;
            
            _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return Instance;
        }
    }
}