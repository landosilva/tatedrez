using Lando.Plugins.Events;
using UnityEngine;
using UnityEngine.Events;
using Event = Lando.Plugins.Events.Event;

namespace Tatedrez.Entities
{   
    public partial class Piece
    {
        public static class Events
        {
            public class Hold : IEvent
            {
                public Piece Piece { get; }
            
                public Hold(Piece piece) => Piece = piece;
            }
        
            public class Release : IEvent
            {
                public Piece Piece { get; }
            
                public Release(Piece piece) => Piece = piece;
            }
        }
        
        [field: SerializeField] public UnityEvent OnHeld { get; private set; }
        [field: SerializeField] public UnityEvent<Vector3> OnReleased { get; private set; }

        private void SubscribeEvents()
        {
            OnHeld.AddListener(NotifyHold);
            OnReleased.AddListener(NotifyRelease);
        }
        
        private void UnsubscribeEvents()
        {
            OnHeld.RemoveListener(NotifyHold);
            OnReleased.RemoveListener(NotifyRelease);
        }

        private void NotifyHold()
        { 
            Events.Hold onHoldEvent = new(piece: this);
            Event.Raise(onHoldEvent);
        }
        
        private void NotifyRelease(Vector3 released)
        {
            Events.Release onReleaseEvent = new(piece: this);
            Event.Raise(onReleaseEvent);
        }
    }
}
