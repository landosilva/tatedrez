using DG.Tweening;
using Lando.Plugins.Singletons.MonoBehaviour;
using UnityEngine;
using PixelPerfectCamera = UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera;

namespace Tatedrez.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PixelPerfectCamera _pixelPerfectCamera;

        [SerializeField] private Vector2Int _zoomOut;
        [SerializeField] private Vector2Int _zoomIn;
        
        public static Vector3 ScreenToWorld(Vector3 screenPosition)
        {
            return Instance._camera.ScreenToWorldPoint(screenPosition);
        }
        
        private static void Zoom(Vector2Int zoom, float duration)
        {
            int x = _instance._pixelPerfectCamera.refResolutionX;
            int y = _instance._pixelPerfectCamera.refResolutionY;
            Vector2Int currentZoom = new(x, y);

            DOTween.Kill(_instance);
            DOVirtual.Vector2(currentZoom, zoom, duration, SetZoom)
                .SetId(_instance);
            
            return;
            
            void SetZoom(Vector2 value)
            {
                _instance._pixelPerfectCamera.refResolutionX = (int)value.x;
                _instance._pixelPerfectCamera.refResolutionY = (int)value.y;
            }
        }
        
        public static void ZoomIn(float duration = 0.5f) => Zoom(_instance._zoomIn, duration);
        public static void ZoomOut(float duration = 0.5f) => Zoom(_instance._zoomOut, duration);
    }
}
