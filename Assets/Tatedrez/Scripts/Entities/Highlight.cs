using System;
using Lando.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class Highlight : MonoBehaviour
    {
        [Serializable]
        public class HighlightPiece
        {
            [SerializeField] private Transform _piece;
            [SerializeField] private Vector2Int _end;
            
            private Vector3 _initial;
            private Vector3 _endInUnits;
            
            public void Awake(int distance)
            {
                _initial = _piece.localPosition;
                _endInUnits = _initial.Add((_end * distance).ToUnits());
            }

            public void Update(float t) => 
                _piece.localPosition = Vector3.Lerp(_initial, _endInUnits, t);
        }
        
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _length = 1f;
        [SerializeField] private int _distance = 1;
        
        [SerializeField] private HighlightPiece[] _pieces;
        
        private void Awake()
        {
            foreach (HighlightPiece piece in _pieces) 
                piece.Awake(_distance);
        }

        private void Update()
        {
            float t = Mathf.PingPong(Time.time * _speed, length: _length);
            foreach (HighlightPiece piece in _pieces) 
                piece.Update(t);
        }
    }
}