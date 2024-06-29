using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Tatedrez.Managers
{
    public class MirrorCamera : MonoBehaviour
    {
        [SerializeField] private PixelPerfectCamera _self;
        [SerializeField] private PixelPerfectCamera _target;

        private void LateUpdate()
        {
            _self.refResolutionX = _target.refResolutionX;
            _self.refResolutionY = _target.refResolutionY;
        }
    }
}