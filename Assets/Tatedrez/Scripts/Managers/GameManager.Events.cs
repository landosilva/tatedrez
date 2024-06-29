using Lando.Plugins.Events;
using Tatedrez.Entities;
using UnityEngine.Events;

namespace Tatedrez.Managers
{   
    public partial class GameManager
    {   
        public class Events
        {
            public class Started : IEvent
            {
            }
            
            public class Over : IEvent
            {
                public PlayerSpot Winner { get; }
                
                public Over (PlayerSpot winner) => Winner = winner;
            }
        }
        
        public UnityEvent OnStarted;
        public UnityEvent<PlayerSpot> OnOver;

        private void SubscribeEvents()
        {
            OnStarted.AddListener(NotifyStarted);
            OnOver.AddListener(NotifyOver);
        }
        
        private void UnsubscribeEvents()
        {
            OnStarted.RemoveListener(NotifyStarted);
            OnOver.RemoveListener(NotifyOver);
        }

        private void NotifyStarted()
        { 
            Events.Started onStarted = new();
            Event.Raise(onStarted);
        }
        
        private void NotifyOver(PlayerSpot winner)
        {
            Events.Over onOver = new(winner);
            Event.Raise(onOver);
        }
    }
}
