

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

namespace Lando.Plugins.Singletons.MonoBehaviour
{
    public interface ISingleton
    {
        bool Initialized { get; }
    }
    
    public abstract class Singleton<T> : UnityEngine.MonoBehaviour, ISingleton where T : Singleton<T>
    {   
        public static T Instance { get; private set; }
        public bool Initialized => Instance != null;
        
        protected virtual void Awake()
        {
            if (Initialized)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = (T) this;
        }

        protected virtual void OnDestroy() { }
    }
    
    public abstract class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>
    {
        protected override void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}