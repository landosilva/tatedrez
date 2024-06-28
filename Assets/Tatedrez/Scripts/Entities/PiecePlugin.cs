using UnityEngine;

namespace Tatedrez.Entities
{
    public abstract class PiecePlugin : MonoBehaviour
    {
        protected Piece _piece;

        private void Awake() => _piece = GetComponentInParent<Piece>();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();
        protected abstract void SubscribeEvents();
        protected abstract void UnsubscribeEvents();
    }
}