using Lando.Plugins.Debugger;
using Tatedrez.Entities;
using Tatedrez.Managers;

namespace Tatedrez.StateMachine.States.Game
{
    public class GameOverState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            PlayerSpot winner = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
            Debugger.Log($"Player with {winner.Style.ToString()} is the winner!");
        }
    }
}