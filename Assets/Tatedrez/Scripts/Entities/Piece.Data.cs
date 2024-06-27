namespace Tatedrez.Entities
{   
    public partial class Piece
    {   
        public enum Type
        {
            Pawn,
            Rook,
            Knight,
            Bishop,
            Queen,
            King
        }
        
        public enum Style
        {
            White,
            Black
        }
    }
}
