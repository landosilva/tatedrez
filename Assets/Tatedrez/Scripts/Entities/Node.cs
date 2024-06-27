using System.Collections.Generic;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;

        private Piece _piece;
        
        private readonly Dictionary<Vector2Int, Node> _neighbours = new();
        
        public bool IsEmpty => _piece == null;
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
            _piece = piece;
            _piece.transform.position = transform.position;
        }
        
        public void Clear()
        {
            _piece = null;
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