using JetBrains.Annotations;
using Lando.Plugins.Debugger;
using Tatedrez.Data;
using Tatedrez.Entities;
using Tatedrez.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tatedrez.Managers
{   
    public partial class GameManager : MonoBehaviour
    {   
        [Header("Players")]
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private PlayerSpot[] _playerSpots;
        
        [Header("Board")]
        [SerializeField] private Board _board;
        [SerializeField] private Strategy[] _winConditions;
        
        [Header("State Machine")]
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private Animator _stateMachine;

        private void Awake()
        {
            InitializeBlackboard();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void InitializeBlackboard()
        {
            _blackboard.Set(this);
            _blackboard.Set(_playerInputManager);
            _blackboard.Set(_playerSpots);
            _blackboard.Set(_board);
            _blackboard.Set(_winConditions);
        }

        public void StartGame()
        {
            foreach (PlayerSpot playerSpot in _playerSpots)
            {
                playerSpot.Reset();
                _board.Reset();
            }
            
            _stateMachine.SetBool(name: States.Started, true);
            
            NotifyStarted();
        }
        
        public void PassTurn()
        {
            _stateMachine.SetTrigger(name: Triggers.Turn.Ended);
        }
        
        public void GameOver(PlayerSpot winner)
        {
            _stateMachine.SetBool(name: States.Over, true);
            
            NotifyOver(winner);
        }
        
        [UsedImplicitly]
        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debugger.Log("Player joined with index: " + playerInput.playerIndex);
            
            Player player = playerInput.GetComponent<Player>();
            
            PlayerSpot playerSpot = _playerSpots[playerInput.playerIndex];
            playerSpot.name += $" [{playerSpot.Style.ToString()}]";
            playerSpot.SetPlayer(player);
            
            if (playerInput.playerIndex == 0)
                playerInput.currentActionMap.Enable();
            else
                playerInput.currentActionMap.Disable();
        }
        
        [UsedImplicitly]
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debugger.Log("Player left with index: " + playerInput.playerIndex);
        }

        public static class Variables
        {
            public static class Player
            {
                public static readonly string Initial = "InitialPlayer";
                public static readonly string Current = "CurrentPlayer";
            }
        }
        
        public static class States
        {
            public static readonly string Bootstrapped = "Bootstrapped";
            public static readonly string Started = "Started";
            public static readonly string Over = "GameOver";
        }
        
        public static class Triggers
        {
            public static class Turn
            {
                public static readonly string Ended = "TurnEnded";
            }
        }
    }
}
