using System.Collections.Generic;
using Lando.Core.Extensions;
using Tatedrez.Extensions;
using UnityEngine;
using Event = Lando.Plugins.Events.Event;

namespace Tatedrez.Entities
{
    public class Board : MonoBehaviour
    {
        [SerializeField, Tooltip("Grid size in units")] private Vector2Int _size;
        [SerializeField, Tooltip("Offset in pixels")] private Vector2Int _offset;
        
        [SerializeField] private Node _nodePrefab;
        
        private readonly Dictionary<Vector2Int, Node> _map = new();

        private void Start() => CreateNodes();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        private void CreateNodes()
        {
            _map.Clear();
            
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    Vector2Int index = new(x, y);
                    IndexToWorld(index, out Vector3 worldPosition);
                    
                    Node node = Instantiate(_nodePrefab, transform);
                    node.transform.position = worldPosition;
                    node.Initialize(index);
                    
                    _map.Add(index, node);
                }
            }
            
            foreach (Node node in _map.Values) 
                node.SetNeighbours(board: this);
        }

        private void IndexToWorld(Vector2Int index, out Vector3 result)
        {
            Vector2 unitOffset = _offset.ToUnits();
            result = index.Add(unitOffset);
        }
        
        private void WorldToIndex(Vector3 worldPosition, out Vector2Int result, bool clamp = true)
        {
            Vector2Int inPixels = worldPosition.ToPixels() - _offset;
            result = inPixels.Divide(Constants.PixelsPerUnit);
            
            if (clamp)
                result.Clamp(min: Vector2Int.zero, max: _size - Vector2Int.one);
        }
        
        private void WorldToNode(Vector3 worldPosition, out Node result, bool clamp = true)
        {
            WorldToIndex(worldPosition, out Vector2Int index, clamp);
            _map.TryGetValue(index, out result);
        }
        
        public bool TryGetNode(Vector2Int index, out Node result) => _map.TryGetValue(index, out result);
        
        private void SubscribeEvents()
        {
            Event.Subscribe<Piece.Events.Hold>(OnPieceHold);
            Event.Subscribe<Piece.Events.Release>(OnPieceRelease);
        }
        
        private void UnsubscribeEvents()
        {
            Event.Unsubscribe<Piece.Events.Hold>(OnPieceHold);
            Event.Unsubscribe<Piece.Events.Release>(OnPieceRelease);
        }

        public bool TryGetPieceMovement(Piece piece, out List<Node> result)
        {
            WorldToIndex(piece.Position, out Vector2Int index, clamp: false);
            bool placed = TryGetNode(index, out Node originNode);
            
            result = placed
                ? piece.Movement.GetMovement(board: this, originNode)
                : new List<Node>(_map.Values);
            
            return result.Count > 0;
        }

        private void OnPieceHold(Piece.Events.Hold e)
        {
            Piece piece = e.Piece;
            
            TryGetPieceMovement(piece, out List<Node> toHighlight);
            
            foreach (Node node in toHighlight)
                node.Highlight();
        }
        
        private void OnPieceRelease(Piece.Events.Release e)
        {
            Piece piece = e.Piece;

            WorldToNode(e.Piece.Position, out Node origin, clamp: false);
            WorldToNode(e.Piece.View, out Node destination);
            
            bool wasHighlighted = destination.IsHighlighted;
            foreach (Node node in _map.Values) 
                node.Unhighlight();
            
            if (!destination.IsEmpty || !wasHighlighted)
                return;
            
            if (origin)
                origin.Clear();
            
            destination.Place(piece);
        }

        public bool ContainsPiece(Piece piece)
        {
            WorldToNode(piece.Position, out Node node, clamp: false);
            return node && node.Piece == piece;
        }

        public void Reset()
        {
            foreach (Node node in _map.Values)
                node.Clear();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white.With(a: 0.5f);
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    Vector2Int position = new(x, y);
                    IndexToWorld(position, out Vector3 worldPosition);
                    Gizmos.DrawCube(center: worldPosition, size: Vector3.one * 0.9f);
                }
            }
        }
    }
}
