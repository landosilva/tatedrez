using System.Collections.Generic;
using Tatedrez.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _vfxPlace;

        private readonly Dictionary<Vector2Int, Node> _neighbours = new();
        
        public Piece Piece { get; private set; }
        public bool IsEmpty => Piece == null;
        public bool IsHighlighted => _highlight.activeSelf;
        
        public Vector2Int Index { get; private set; }

        public void Initialize(Vector2Int index)
        {
            Index = index;
            gameObject.name = $"Node ({index.x}, {index.y})";
        }

        private void Awake()
        {
            _highlight.SetActive(false);
        }
        
        public void Place(Piece piece)
        {
            Vector3 offset = piece.PlacementOffset.ToUnits();
            Vector3 destination = transform.position + offset;
            Piece = piece;
            Piece.transform.position = destination;
            
            GameObject vfx = Instantiate(_vfxPlace, transform.position, Quaternion.identity);
            Destroy(vfx, t: 2);
        }
        
        public void Clear()
        {
            Piece = null;
        }
        
        public void Highlight()
        {
            if (!IsEmpty)
                return;
            
            _highlight.SetActive(true);
        }
        
        public void Unhighlight()
        {
            _highlight.SetActive(false);
        }
        
        public void SetNeighbours(Board board)
        {
            foreach (Vector2Int direction in Direction.All)
            {
                Vector2Int neighbourIndex = Index + direction;
                if (board.TryGetNode(neighbourIndex, out Node neighbour))
                    _neighbours.Add(direction, neighbour);
            }
        }
        
        public Node GetNeighbour(Vector2Int direction) => _neighbours.GetValueOrDefault(direction);
        
        public static class Direction
        {
            public static Vector2Int Up => new(0, 1);
            public static Vector2Int Down => new(0, -1);
            public static Vector2Int Left => new(-1, 0);
            public static Vector2Int Right => new(1, 0);
            
            public static Vector2Int UpLeft => new(-1, 1);
            public static Vector2Int UpRight => new(1, 1);
            public static Vector2Int DownLeft => new(-1, -1);
            public static Vector2Int DownRight => new(1, -1);
            
            public static List<Vector2Int> All => new()
            {
                Up, Down, Left, Right,
                UpLeft, UpRight, DownLeft, DownRight
            };
        }
    }
}