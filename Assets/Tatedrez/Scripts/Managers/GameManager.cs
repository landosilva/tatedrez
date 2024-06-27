using JetBrains.Annotations;
using Lando.Plugins.Debugger;
using Tatedrez.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tatedrez.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpot[] _playerSpots;
        [SerializeField] private PlayerInputManager _playerInputManager;

        private void Start()
        {
            for (int i = 0; i < _playerSpots.Length; i++)
            {
                Touchscreen touchscreen = InputSystem.GetDevice<Touchscreen>();
                _playerInputManager.JoinPlayer(i, pairWithDevice: touchscreen);
            }
        }

        [UsedImplicitly]
        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debugger.Log("Player joined with index: " + playerInput.playerIndex);
            
            Player player = playerInput.GetComponent<Player>();
            
            PlayerSpot playerSpot = _playerSpots[playerInput.playerIndex];
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
    }
}
