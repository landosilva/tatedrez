using System;
using JetBrains.Annotations;
using Tatedrez.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tatedrez.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _playerSpots;
        [SerializeField] private PlayerInputManager _playerInputManager;
        
        private Touchscreen[] _touchscreens;

        private void Awake()
        {
            _touchscreens = new Touchscreen[_playerSpots.Length];
        }

        private void Start()
        {
            for (int i = 0; i < _playerSpots.Length; i++)
            {
                Touchscreen touchscreen = InputSystem.AddDevice<Touchscreen>(name: $"Player {i} Touchscreen");
                _touchscreens[i] = touchscreen;
                _playerInputManager.JoinPlayer(i, pairWithDevice: touchscreen);
            }
        }

        private void OnDestroy()
        {
            foreach (Touchscreen touchscreen in _touchscreens)
                InputSystem.RemoveDevice(touchscreen);
        }

        [UsedImplicitly]
        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debug.Log("Player joined with index: " + playerInput.playerIndex);
            
            Transform playerSpot = _playerSpots[playerInput.playerIndex];
            
            Player player = playerInput.GetComponent<Player>();
            player.transform.SetParent(playerSpot);
            player.transform.localPosition = Vector3.zero;

            Array styles = Enum.GetValues(typeof(Piece.Style));
            Piece.Style style = (Piece.Style)styles.GetValue(playerInput.playerIndex); 
            player.SetStyle(style);
        }
        
        [UsedImplicitly]
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log("Player left with index: " + playerInput.playerIndex);
        }
    }
}
