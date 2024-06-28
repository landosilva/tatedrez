using DG.Tweening;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class PieceVFX : PiecePlugin
    {
        private const string ID = "Piece";
        
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
        
        private void OnPieceHeld()
        {
            const float scale = 1.1f;
            const float duration = 0.1f;
            
            DOTween.Kill(ID);
            
            _piece.transform.DOScale(endValue: scale, duration)
                .SetEase(Ease.OutBack)
                .SetId(ID);
        }
        
        private void OnPieceReleased(Vector3 released)
        {
            const float stretch = 1.1f;
            const float squish = 0.9f;
            
            const float squishDuration = 0.1f;
            const float bounceDuration = 0.3f;

            DOTween.Kill(ID);
            
            _piece.transform.localScale = Vector3.one;

            Vector3 endValue = new(stretch, squish, 0);
            Sequence bounce = DOTween.Sequence().SetId(ID);
            bounce.Append(_piece.transform.DOScale(endValue, squishDuration).SetEase(Ease.OutBack));
            bounce.Append(_piece.transform.DOScale(Vector3.one, bounceDuration).SetEase(Ease.OutBack));
        }
    }
}