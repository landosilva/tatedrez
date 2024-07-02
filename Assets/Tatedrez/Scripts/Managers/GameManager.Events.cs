using Lando.Plugins.Events;
using Tatedrez.Entities;
using UnityEngine;
using UnityEngine.Events;
using Event = Lando.Plugins.Events.Event;

namespace Tatedrez.Managers
{   
    public partial class GameManager
    {   
        public static class Events
        {
            public class Started : IEvent
            {
            }
            
            public class Over : IEvent
            {
                public PlayerSpot Winner { get; }
                
                public Over (PlayerSpot winner) => Winner = winner;
            }

            public class Tick : IEvent
            {
                public PlayerSpot[] Players { get; }

                public Tick(PlayerSpot[] players) => Players = players;
            }
        }

        [field: SerializeField] public UnityEvent OnStarted { get; private set; }
        [field: SerializeField] public UnityEvent<PlayerSpot> OnOver { get; private set; }

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
