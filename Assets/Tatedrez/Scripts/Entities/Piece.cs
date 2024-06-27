using System.Collections;
using Tatedrez.Data;
using UnityEngine;

namespace Tatedrez.Entities
{   
    public partial class Piece : MonoBehaviour
    {   
        [SerializeField] private Type _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Movement _movement;
        
        private Style _style;
        private Coroutine _followCoroutine;
        
        public Movement Movement => _movement;
        
        public Vector3 Position => transform.position;
        public Vector3 ViewPosition => _spriteRenderer.transform.position;

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
                    _spriteRenderer.transform.position = target.position + offset;
                    yield return null;
                }
            }
        }

        public void Release()
        {
            StopFollowing();
            NotifyRelease();
            
            _spriteRenderer.transform.localPosition = Vector3.zero;
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
