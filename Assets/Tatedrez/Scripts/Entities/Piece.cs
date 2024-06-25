using Tatedrez.Data;
using UnityEngine;

namespace Tatedrez.Entities
{   
    public class Piece : MonoBehaviour
    {   
        [SerializeField] private Type _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Style _style;
        
        public void SetStyle(Style style)
        {
            _style = style;
            _spriteRenderer.sprite = PiecesDatabase.GetSprite(style, _type);
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
