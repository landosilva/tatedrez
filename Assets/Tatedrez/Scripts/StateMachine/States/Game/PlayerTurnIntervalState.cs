using System;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine;

namespace Tatedrez.StateMachine.States.Game
{
    public class PlayerTurnIntervalState : GameState
    {
        [SerializeField] private float _duration = 1f;
        
        private float _enterTime;
        
        protected override void OnEnter()
        {
            base.OnEnter();
            _enterTime = Time.time;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            bool intervalEnded = Time.time - _enterTime >= _duration;
            if (!intervalEnded)
                return;
            
            _stateMachine.SetTrigger(name: GameManager.Triggers.Turn.Ended);
        }

        protected override void OnExit()
        {
            base.OnExit();
            
            PlayerSpot[] playerSpots = _blackboard.Get<PlayerSpot[]>();
            PlayerSpot currentPlayer = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            int playerIndex = Array.IndexOf(playerSpots, currentPlayer);
            int nextPlayerIndex = (playerIndex + 1) % playerSpots.Length;
            PlayerSpot nextPlayer = playerSpots[nextPlayerIndex];
            _blackboard.Set(GameManager.Variables.Player.Current, nextPlayer);
        }
    }
}