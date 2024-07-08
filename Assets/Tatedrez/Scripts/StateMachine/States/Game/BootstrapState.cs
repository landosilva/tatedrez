using Lando.Plugins.Debugger;
using Lando.Plugins.Sound;
using Tatedrez.Entities;
using Tatedrez.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Tatedrez.StateMachine.States.Game
{
    public class BootstrapState : GameState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            
            PlayerSpot[] playerSpots = _blackboard.Get<PlayerSpot[]>();
            PlayerInputManager playerInputManager = _blackboard.Get<PlayerInputManager>();

            InputDevice inputDevice = InputSystem.GetDevice<Touchscreen>();
            
            if (inputDevice == null || Application.platform == RuntimePlatform.WebGLPlayer)
            {
                InputSystem.onDeviceChange += OnDeviceChange;
                GameObject touchSimulation = new(name: "Touch Simulation", components: typeof(TouchSimulation));
                DontDestroyOnLoad(touchSimulation);
            }
            else
                CreatePlayers();
            
            _stateMachine.SetBool(name: GameManager.States.Bootstrapped, true);
            
            SoundManager.PlayMusic(SoundDatabase.Music.Background);
            SoundManager.SetMusicLayerVolume(0, volume: 0.2f);
            
            return;
            
            void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                if (change != InputDeviceChange.Added || device is not Touchscreen) 
                    return;
                
                Debugger.Log("Adding simulated touch controls.");
                InputSystem.onDeviceChange -= OnDeviceChange;
                    
                inputDevice = device;
                CreatePlayers();
            }
            
            void CreatePlayers()
            {
                Debugger.Log("Creating players with device: " + inputDevice.name);
                
                for (int i = 0; i < playerSpots.Length; i++)
                {
                    playerInputManager.JoinPlayer(i, pairWithDevice: inputDevice);
                
                    PlayerSpot playerSpot = playerSpots[i];
                    foreach (Piece piece in playerSpot.Pieces) 
                        piece.Unhighlight();
                }
            }
        }
    }
}