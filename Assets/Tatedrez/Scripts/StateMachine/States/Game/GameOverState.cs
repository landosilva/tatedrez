using Lando.Plugins.Debugger;
using Lando.Plugins.Sound;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Tatedrez.StateMachine.States.Game
{
    public class GameOverState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            PlayerSpot winner = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            Debugger.Log($"Player with {winner.Style.ToString()}s is the winner!");
            
            CameraManager.ZoomOut();
            
            SoundManager.PlaySFX(SoundDatabase.Game.GameOver);
            
            InputSystem.onEvent += OnInputEvent;
        }

        private void OnInputEvent(InputEventPtr inputEvent, InputDevice inputDevice)
        {
            if (!inputEvent.HasButtonPress())
                return;
            if (inputDevice is not (Touchscreen or Mouse))
                return;
            
            InputSystem.onEvent -= OnInputEvent;
            
            GameManager gameManager = _blackboard.Get<GameManager>();
            gameManager.StartGame();
        }

        protected override void OnExit()
        {
            base.OnExit();
            
            InputSystem.onEvent -= OnInputEvent;
            
            _stateMachine.SetBool(name: GameManager.States.Started, false);
            _stateMachine.SetBool(name: GameManager.States.Over, false);
        }
    }
}