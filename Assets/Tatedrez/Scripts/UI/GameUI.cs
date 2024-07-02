using System.Linq;
using Lando.Core.Extensions;
using Tatedrez.Entities;
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
            Event.Subscribe<GameManager.Events.Tick>(OnGameTick);
        }
        
        private void OnDisable()
        {
            Event.Unsubscribe<GameManager.Events.Started>(OnGameStarted);
            Event.Unsubscribe<GameManager.Events.Over>(OnGameOver);
            Event.Unsubscribe<GameManager.Events.Tick>(OnGameTick);
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
        
        private void OnGameTick(GameManager.Events.Tick e)
        {
            const string separator = "  ";
            string timer = e.Players.Aggregate(string.Empty, PlayerTimers).TrimEnd(separator.ToCharArray());
            _message.text = timer.MSpace();
            _message.gameObject.SetActive(true);
            
            return;

            string PlayerTimers(string current, PlayerSpot playerSpot)
            {
                return $"{current}<sprite index=0 color=#{playerSpot.Color}> {playerSpot.Timer.TotalSeconds:0.00}s{separator}";
            }
        } 
    }
}
