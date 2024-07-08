using Tatedrez.Managers;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Tatedrez.StateMachine.States.Game
{
    public class InitialState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            
            _stateMachine.SetBool(name: GameManager.States.Started, false);
            _stateMachine.SetBool(name: GameManager.States.Over, false);
            
            InputSystem.onEvent += OnInputEvent;
        }

        private void OnInputEvent(InputEventPtr inputEvent, InputDevice inputDevice)
        {
            if (!inputEvent.HasButtonPress())
                return;
            if (inputDevice is not (Touchscreen or Mouse))
                return;
            
            GameManager gameManager = _blackboard.Get<GameManager>();
            gameManager.StartGame();
        }
        
        protected override void OnExit()
        {
            base.OnExit();
            
            InputSystem.onEvent -= OnInputEvent;
            CameraManager.ZoomIn();
        }
    }
}