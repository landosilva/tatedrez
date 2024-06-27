using Tatedrez.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Tatedrez.Entities
{
    public class Player : MonoBehaviour
    {   
        [SerializeField] private Transform _finger;
        
        private Piece.Style _style;
        private Piece _holdingPiece;
        
        private readonly Collider2D[] _buffer = new Collider2D[1];
        
        public void OnTouch(InputAction.CallbackContext context)
        {
            if (!context.performed) 
                return;
            
            TouchState touchState = (TouchState)context.ReadValueAsObject();
            switch (touchState.phase)
            {
                default:
                case TouchPhase.None:
                case TouchPhase.Canceled:
                    break;
                case TouchPhase.Began:
                    OnTouchBegan();
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    OnTouchMoved();
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }

            return;

            void OnTouchBegan()
            {
                DebugManager.Log("Touch began");
                
                _finger.transform.position = CameraManager.ScreenToWorld(touchState.position);
                
                if (!TryGetPiece(out Piece piece))
                    return;
                
                piece.Follow(_finger);
                _holdingPiece = piece;
            }
            
            void OnTouchMoved()
            {
                _finger.transform.position = CameraManager.ScreenToWorld(touchState.position);
            }
            
            void OnTouchEnded()
            {
                DebugManager.Log("Touch ended");
                
                if (_holdingPiece == null)
                    return;
                
                _holdingPiece.Release();
            }

            bool TryGetPiece(out Piece piece)
            {
                Vector3 position = CameraManager.ScreenToWorld(touchState.position);

                int overlapped = Physics2D.OverlapCircleNonAlloc(position, radius: 0.1f, _buffer, Layer.Mask.Piece);
                if (overlapped == 0)
                {
                    piece = null;
                    return false;
                }

                piece = _buffer[0].GetComponentInParent<Piece>();
                return true;
            }
        }
    }
}
