using System;
using System.Collections.Generic;
using System.Linq;
using Tatedrez.Data;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine;

namespace Tatedrez.StateMachine.States.Game
{
    public class PlayerTurnIntervalState : GameState
    {
        [SerializeField] private float _duration = 1f;
        
        private float _enterTime;
        
        private bool IntervalEnded => Time.time - _enterTime >= _duration;
        
        protected override void OnEnter()
        {
            base.OnEnter();
            _enterTime = Time.time;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            if (!IntervalEnded)
                return;
            
            CheckBoard();
        }

        private void CheckBoard()
        {
            Board board = _blackboard.Get<Board>();
            Strategy[] winConditions = _blackboard.Get<Strategy[]>();

            PlayerSpot[] players = _blackboard.Get<PlayerSpot[]>();
            GameManager gameManager = _blackboard.Get<GameManager>();

            if (players.Any(WithoutTimeLeft))
            {
                PlayerSpot winner = players.First(WithTimeLeft);
                gameManager.GameOver(winner);
                return;
            }

            foreach (Strategy condition in winConditions)
            {
                IEnumerable<Node> nodes = condition.GetNodes(board).Where(HavePiece);
                if(nodes.Count() < condition.Count)
                    continue;
                
                Piece.Style style = nodes.ElementAt(0).Piece.GetStyle();

                if (nodes.All(HaveSameStyle))
                {
                    PlayerSpot winner = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
                    gameManager.GameOver(winner);
                    return;
                }
                
                continue;

                bool HavePiece(Node node) => !node.IsEmpty;
                bool HaveSameStyle(Node node) => node.Piece.GetStyle() == style;
            }
            
            SetNextPlayer();
            gameManager.PassTurn();
            
            return;

            bool WithTimeLeft(PlayerSpot player) => !player.IsTimeOver;
            bool WithoutTimeLeft(PlayerSpot player) => player.IsTimeOver;
        }
        
        private void SetNextPlayer()
        {
            PlayerSpot[] playerSpots = _blackboard.Get<PlayerSpot[]>();
            PlayerSpot currentPlayer = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            int playerIndex = Array.IndexOf(playerSpots, currentPlayer);
            int nextPlayerIndex = (playerIndex + 1) % playerSpots.Length;
            PlayerSpot nextPlayer = playerSpots[nextPlayerIndex];
            
            _blackboard.Set(GameManager.Variables.Player.Current, nextPlayer);
        }
    }
}