using UnityEngine;

namespace Tatedrez.Entities
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;
        
        private Piece _piece;
        
        public bool IsEmpty => _piece == null;

        private void Awake()
        {
            _highlight.SetActive(false);
        }
        
        public void Place(Piece piece)
        {
            _piece = piece;
            _piece.transform.position = transform.position;
        }
        
        public void Clear()
        {
            _piece = null;
        }
    }
}