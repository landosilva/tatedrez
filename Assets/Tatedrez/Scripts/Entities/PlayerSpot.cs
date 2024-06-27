using UnityEngine;

namespace Tatedrez.Entities
{
    public class PlayerSpot : MonoBehaviour
    {
        [SerializeField] private Piece.Style _style;
        [SerializeField] private Piece[] _pieces;
        
        private Player _player;

        private void Start()
        {
            SetStyle(_style);
        }

        public void SetPlayer(Player player)
        {
            player.transform.SetParent(transform);
            player.transform.localPosition = Vector3.zero;
        }

        private void SetStyle(Piece.Style style)
        {
            _style = style;
            foreach (Piece piece in _pieces) 
                piece.SetStyle(style);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetStyle(_style);
        }
#endif
    }
}
