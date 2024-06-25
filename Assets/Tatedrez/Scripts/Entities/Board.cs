using System.Collections.Generic;
using Lando.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class Board : MonoBehaviour
    {
        [SerializeField, Tooltip("Grid size in units")] private Vector2Int _size;
        [SerializeField, Tooltip("Offset in pixels")] private Vector2Int _offset;
        
        [SerializeField] private Node _nodePrefab;
        
        private readonly List<Node> _nodes = new();

        private void Start()
        {
            CreateNodes();
        }
        
        private void CreateNodes()
        {
            _nodes.Clear();
            
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    Node node = Instantiate(_nodePrefab, transform);
                    node.name = $"Node ({x}, {y})";
                    Vector2Int position = new(x, y);
                    IndexToWorld(position, out Vector3 worldPosition);
                    node.transform.position = worldPosition;
                    _nodes.Add(node);
                }
            }
        }
        
        public void WorldToNode(Vector3 worldPosition, out Node result)
        {
            float minSqrMagnitude = float.MaxValue;
            
            foreach (Node node in _nodes)
            {
                float sqrMagnitude = (worldPosition - node.transform.position).sqrMagnitude;
                if (sqrMagnitude >= minSqrMagnitude)
                    continue;
                
                minSqrMagnitude = sqrMagnitude;
                result = node;
            }
            
            result = null;
        }

        private void IndexToWorld(Vector2Int index, out Vector3 result)
        {
            Vector2 unitOffset = _offset.ToUnits();
            result = index.Add(unitOffset);
        }

#if UNITY_EDITOR
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
#endif
    }
}
