using Lando.Core.Extensions;
using Lando.Plugins.Sound;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine.InputSystem;

namespace Tatedrez.StateMachine.States.Game
{
    public class BootstrapState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            
            PlayerSpot[] playerSpots = _blackboard.Get<PlayerSpot[]>();
            PlayerInputManager playerInputManager = _blackboard.Get<PlayerInputManager>();
            
            for (int i = 0; i < playerSpots.Length; i++)
            {
                Touchscreen touchscreen = InputSystem.GetDevice<Touchscreen>();
                playerInputManager.JoinPlayer(i, pairWithDevice: touchscreen);
                
                PlayerSpot playerSpot = playerSpots[i];
                foreach (Piece piece in playerSpot.Pieces) 
                    piece.Unhighlight();
            }
            
            PlayerSpot initialPlayer = playerSpots.PickRandom();
            
            _blackboard.Set(GameManager.Variables.Player.Initial, initialPlayer);
            _blackboard.Set(GameManager.Variables.Player.Current, initialPlayer);
            
            _stateMachine.SetBool(name: GameManager.States.Bootstrapped, true);
            
            SoundManager.PlayMusic(SoundDatabase.Music.Background);
            SoundManager.SetMusicLayerVolume(0, volume: 0.2f);
        }
    }
}