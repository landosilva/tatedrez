using System;
using Lando.Core.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class PlayerSpot : MonoBehaviour
    {
        [SerializeField] private Piece.Style _style;
        [SerializeField] private Piece[] _pieces;
        [SerializeField] private int _durationInSeconds = 60;
        [SerializeField] private Color _color;

        public Piece.Style Style => _style;
        public Piece[] Pieces => _pieces;
        
        public bool IsReady { get; private set; }
        public TimeSpan Timer { get; private set; }
        public bool IsTimeOver => Timer.Ticks <= 0;
        public string Color => _color.ToHex();

        private void Start()
        {
            SetStyle(_style);
        }

        public void SetPlayer(Player player)
        {
            player.transform.SetParent(transform);
            player.transform.localPosition = Vector3.zero;
        }

        private void SetStyle(Piece.Style style)
        {
            _style = style;
            foreach (Piece piece in _pieces) 
                piece.SetStyle(style);
        }
        
        public void SetReady(bool ready)
        {
            IsReady = ready;
        }

        public void Reset()
        {
            foreach (Piece piece in _pieces)
                piece.Reset();
            
            SetReady(false);
            Timer = TimeSpan.FromSeconds(_durationInSeconds);
        }

        public void Tick(float deltaTime)
        {
            Timer = Timer.Subtract(TimeSpan.FromSeconds(deltaTime));
        }
    }
}
