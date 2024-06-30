using Lando.Plugins.Sound;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class PieceSFX : PiecePlugin
    {
        protected override void SubscribeEvents()
        {
            _piece.OnHeld.AddListener(OnPieceHeld);
            _piece.OnReleased.AddListener(OnPieceReleased);
        }

        protected override void UnsubscribeEvents()
        {
            _piece.OnHeld.RemoveListener(OnPieceHeld);
            _piece.OnReleased.RemoveListener(OnPieceReleased);
        }
        
        private static void OnPieceHeld()
        {
            SoundManager.PlaySFX(SoundDatabase.Piece.Hold);
        }
        
        private static void OnPieceReleased(Vector3 released)
        {
            SoundManager.PlaySFX(SoundDatabase.Piece.Place);
        }
    }
}