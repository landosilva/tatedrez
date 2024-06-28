using System.Linq;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine;

namespace Tatedrez.Behaviours
{
    public class PlayerTurnState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            AddListeners();
        }
        
        protected override void OnExit()
        {
            base.OnExit();
            RemoveListeners();
            
            PlayerSpot playerSpot = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            Board board = _blackboard.Get<Board>();

            if (playerSpot.Pieces.All(PlacedOnBoard))
                playerSpot.SetReady(true);
            
            return;

            bool PlacedOnBoard(Piece piece) => board.ContainsPiece(piece);
        }

        private void AddListeners()
        {
            PlayerSpot playerSpot = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            Board board = _blackboard.Get<Board>();
            foreach (Piece piece in playerSpot.Pieces)
            {
                if(!playerSpot.IsReady && board.ContainsPiece(piece))
                    continue;
                
                piece.Highlight();
                
                piece.OnHeld.AddListener(OnPieceHeld);
                piece.OnReleased.AddListener(OnPieceReleased);
            }
        }
        
        private void RemoveListeners()
        {
            PlayerSpot playerSpot = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            foreach (Piece piece in playerSpot.Pieces)
            {
                piece.Unhighlight();
                
                piece.OnHeld.RemoveListener(OnPieceHeld);
                piece.OnReleased.RemoveListener(OnPieceReleased);
            }
        }
        
        private void OnPieceHeld()
        {
            PlayerSpot playerSpot = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            foreach (Piece piece in playerSpot.Pieces)
            {
                piece.Unhighlight();
                piece.OnHeld.RemoveListener(OnPieceHeld);
            }
        }
        
        private void OnPieceReleased(Vector3 position)
        {
            _stateMachine.SetTrigger(name: GameManager.Triggers.Turn.Ended);
        }
    }
}