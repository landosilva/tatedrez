using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tatedrez.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        
        private void Start()
        {
            _playerInputManager.JoinPlayer();
        }

        [UsedImplicitly]
        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debug.Log("Player joined with index: " + playerInput.playerIndex);
        }
        
        [UsedImplicitly]
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log("Player left with index: " + playerInput.playerIndex);
        }
    }
}
