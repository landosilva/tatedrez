using System.Collections;
using Tatedrez.Data;
using UnityEngine;

namespace Tatedrez.Entities
{   
    public class Piece : MonoBehaviour
    {   
        [SerializeField] private Type _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Style _style;
        private Vector3 _origin;
        private Vector3 _last;
        private Coroutine _followCoroutine;
        
        private readonly Collider2D[] _buffer = new Collider2D[1];

        private void Awake()
        {
            _origin = transform.position;
        }

        public void SetStyle(Style style)
        {
            _style = style;
            _spriteRenderer.sprite = PiecesDatabase.GetSprite(style, _type);
        }
        
        public void Follow(Transform target)
        {
            if (TryGetNode(transform.position, out Node node)) 
                node.Clear();
            
            _last = transform.position;
            Vector3 offset = transform.position - target.position;
            
            StopFollowing();
            _followCoroutine = StartCoroutine(routine: FollowEnumerator());
            
            return;

            IEnumerator FollowEnumerator()
            {
                while (true)
                {
                    transform.position = target.position + offset;
                    yield return null;
                }
            }
        }

        public void Release()
        {
            StopFollowing();

            if (TryGetNode(transform.position, out Node node) && node.IsEmpty)
            {
                _last = node.transform.position;
                node.Place(piece: this);
                return;
            }

            if (TryGetNode(_last, out Node lastNode))
            {
                lastNode.Place(piece: this);
                return;
            }
            
            transform.position = _origin;
        }
        
        private void StopFollowing()
        {
            if (_followCoroutine == null) 
                return;
            
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }

        private bool TryGetNode(Vector3 position, out Node node)
        {
            int overlapped = Physics2D.OverlapPointNonAlloc(position, _buffer, Layer.Mask.Node);
            if (overlapped == 0)
            {
                node = null;
                return false;
            }
            node = _buffer[0].GetComponentInParent<Node>();
            return true;
        }
        
        public enum Style
        {
            White = 0,
            Black = 1
        }

        public enum Type
        {
            Pawn = 0,
            Knight = 1,
            Rook = 2,
            Bishop = 3,
            Queen = 4,
            King = 5
        }
    }
}
