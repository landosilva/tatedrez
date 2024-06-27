using Lando.Plugins.Singletons.MonoBehaviour;
using UnityEngine;

namespace Tatedrez.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private Camera _camera;
        
        public static Vector3 ScreenToWorld(Vector3 screenPosition)
        {
            return Instance._camera.ScreenToWorldPoint(screenPosition);
        }
    }
}
