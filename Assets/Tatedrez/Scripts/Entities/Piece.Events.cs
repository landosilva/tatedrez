using Lando.Plugins.Events;
using UnityEngine;
using UnityEngine.Events;
using Event = Lando.Plugins.Events.Event;

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
        
        public UnityEvent OnHeld;
        public UnityEvent<Vector3> OnReleased;

        private void Awake()
        {
            OnHeld.AddListener(NotifyHold);
            OnReleased.AddListener(NotifyRelease);
        }

        private void NotifyHold()
        { 
            HoldEvent onHoldEvent = new(piece: this);
            Event.Raise(onHoldEvent);
        }
        
        private void NotifyRelease(Vector3 released)
        {
            ReleaseEvent onReleaseEvent = new(piece: this);
            Event.Raise(onReleaseEvent);
        }
    }
}
