using Tatedrez.Managers;
using TMPro;
using UnityEngine;
using Event = Lando.Plugins.Events.Event;

namespace Tatedrez.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;
        
        private void OnEnable()
        {
            Event.Subscribe<GameManager.Events.Started>(OnGameStarted);
            Event.Subscribe<GameManager.Events.Over>(OnGameOver);
        }
        
        private void OnDisable()
        {
            Event.Unsubscribe<GameManager.Events.Started>(OnGameStarted);
            Event.Unsubscribe<GameManager.Events.Over>(OnGameOver);
        }
        
        private void OnGameStarted(GameManager.Events.Started e)
        {
            _title.gameObject.SetActive(false);
            _message.gameObject.SetActive(false);
        }
        
        private void OnGameOver(GameManager.Events.Over e)
        {
            _title.SetText($"\n{e.Winner.Style.ToString()}s won!");
            _title.gameObject.SetActive(true);

            _message.text = "Tap to restart";
            _message.gameObject.SetActive(true);
        }
    }
}
