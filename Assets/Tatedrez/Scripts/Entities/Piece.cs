using System.Collections;
using DG.Tweening;
using Tatedrez.Data;
using Tatedrez.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{   
    public partial class Piece : MonoBehaviour
    {   
        [SerializeField] private Type _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Movement _movement;
        [SerializeField] private Vector2Int _placementOffset;
        
        private Style _style;
        private Coroutine _followCoroutine;
        
        public Movement Movement => _movement;
        
        public Vector3 Position => transform.position;
        public Vector3 ViewPosition => _spriteRenderer.transform.position;
        public Vector2Int PlacementOffset => _placementOffset;

        public void SetStyle(Style style)
        {
            _style = style;
            _spriteRenderer.sprite = PiecesDatabase.GetSprite(style, _type);
        }
        
        public void Follow(Transform target)
        {
            Vector3 offset = transform.position - target.position;
            
            StopFollowing();
            _followCoroutine = StartCoroutine(routine: FollowEnumerator());
            
            NotifyHold();
            
            return;

            IEnumerator FollowEnumerator()
            {
                while (true)
                {
                    Vector3 placementOffset = _placementOffset.ToUnits();
                    _spriteRenderer.transform.position = target.position + offset - placementOffset;
                    yield return null;
                }
            }
        }

        public void Release()
        {
            Vector3 position = _spriteRenderer.transform.position;
            StopFollowing();
            NotifyRelease();

            _spriteRenderer.transform.localPosition = Vector3.zero;
            // _spriteRenderer.transform.position = position;
            // _spriteRenderer.transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.OutBounce);

            Vector3 scale = new(1.1f,0.9f,0);
            transform.DOScale(scale, 0.1f).SetEase(Ease.OutBack).SetDelay(0);
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f);
        }
        
        private void StopFollowing()
        {
            if (_followCoroutine == null) 
                return;
            
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }
    }
}
