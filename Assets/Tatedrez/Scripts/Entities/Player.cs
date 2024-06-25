using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Tatedrez.Entities
{
    public class Player : MonoBehaviour
    {   
        [SerializeField] private Piece[] _pieces;
        
        private Piece.Style _style;
        
        public void SetStyle(Piece.Style style)
        {
            _style = style;
            foreach (Piece piece in _pieces) 
                piece.SetStyle(style);
        }
        
        [UsedImplicitly]
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
                // Debug.Log("Touch began at position: " + touchState.position);
            }
            
            void OnTouchMoved()
            {
                // Debug.Log("Touch moved to position: " + touchState.position);
            }
            
            void OnTouchEnded()
            {
                // Debug.Log("Touch ended at position: " + touchState.position);
            }
        }
    }
}
