using Lando.Plugins.Debugger;
using Lando.Plugins.Sound;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

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
            
            _stateMachine.SetBool(name: GameManager.States.Started, false);
            _stateMachine.SetBool(name: GameManager.States.Over, false);
        }
    }
}