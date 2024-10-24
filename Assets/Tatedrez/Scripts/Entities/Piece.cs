using System.Collections;
using Tatedrez.Data;
using Tatedrez.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public partial class Piece : MonoBehaviour
    {   
        [SerializeField] private Type _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Strategy _movement;
        [SerializeField] private Vector2Int _placementOffset;

        [SerializeField] private GameObject _highlight;
        [SerializeField] private Collider2D _collider;
        
        private Style _style;
        private Coroutine _followCoroutine;
        private Vector3 _origin;
        
        public Strategy Movement => _movement;
        
        public Vector3 Position => transform.position;
        public Vector3 View => _spriteRenderer.transform.position;
        public Vector2Int PlacementOffset => _placementOffset;
        
        private void Awake()
        {
            _origin = transform.position;
            SubscribeEvents();
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        public void SetStyle(Style style)
        {
            _style = style;
            _spriteRenderer.sprite = PiecesDatabase.GetSprite(style, _type);
        }
        
        public Style GetStyle() => _style;
        
        public void Follow(Transform target)
        {
            Vector3 offset = transform.position - target.position;
            
            StopFollowing();
            _followCoroutine = StartCoroutine(routine: FollowEnumerator());
            
            OnHeld?.Invoke();
            
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
            Vector3 releasedPosition = _spriteRenderer.transform.position;
            
            StopFollowing();
            OnReleased?.Invoke(releasedPosition);
            
            _spriteRenderer.transform.localPosition = Vector3.zero;
        }
        
        private void StopFollowing()
        {
            if (_followCoroutine == null) 
                return;
            
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }

        public void Highlight()
        {
            _highlight.SetActive(value: true);
            _collider.enabled = true;
        }

        public void Unhighlight()
        {
            _highlight.SetActive(value: false);
            _collider.enabled = false;
        }

        public void Reset()
        {
            transform.position = _origin;
            _spriteRenderer.transform.localPosition = Vector3.zero;
            
            StopFollowing();
            Unhighlight();
        }
    }
}
