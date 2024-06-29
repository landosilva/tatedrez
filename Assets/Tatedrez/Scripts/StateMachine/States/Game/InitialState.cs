using Tatedrez.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tatedrez.StateMachine.States.Game
{
    public class InitialState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            
            _stateMachine.SetBool(name: GameManager.States.Started, false);
            _stateMachine.SetBool(name: GameManager.States.Over, false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (!Touchscreen.current.primaryTouch.isInProgress)
                return;
            
            GameManager gameManager = _blackboard.Get<GameManager>();
            gameManager.StartGame();
        }

        protected override void OnExit()
        {
            base.OnExit();
            
            CameraManager.ZoomIn();
        }
    }
}