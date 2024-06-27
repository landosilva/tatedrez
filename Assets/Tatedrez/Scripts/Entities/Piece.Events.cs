using Lando.Plugins.Events;

namespace Tatedrez.Entities
{   
    public partial class Piece
    {
        public class HoldEvent : IEvent
        {
            public Piece Piece { get; }
            
            public HoldEvent(Piece piece) => Piece = piece;
        }
        
        public class ReleaseEvent : IEvent
        {
            public Piece Piece { get; }
            
            public ReleaseEvent(Piece piece) => Piece = piece;
        }

        private void NotifyHold()
        { 
            HoldEvent onHoldEvent = new(piece: this);
            Event.Raise(onHoldEvent);
        }
        
        private void NotifyRelease()
        {
            ReleaseEvent onReleaseEvent = new(piece: this);
            Event.Raise(onReleaseEvent);
        }
    }
}
