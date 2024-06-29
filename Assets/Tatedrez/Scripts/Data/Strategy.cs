using System.Collections.Generic;
using System.Linq;
using Lando.Core.Extensions;
using Tatedrez.Entities;
using UnityEngine;

namespace Tatedrez.Data
{
    [CreateAssetMenu(fileName = "Strategy", menuName = "Tatedrez/Strategy")]
    public partial class Strategy : ScriptableObject
    {   
        [SerializeField] private List<Vector2Int> _positions;
        
        [SerializeField] private int _range = 1;
        [SerializeField] private bool _isDirectional = true;
        
#if UNITY_EDITOR
        [SerializeField] private bool _disableOrigin = true;
#endif
        
        private bool IsDirectional => _isDirectional;
        public int Count => _positions.Count;

        public List<Node> GetNodes(Board board)
        {
            List<Node> nodes = new();

            foreach (Vector2Int index in _positions.Select(AsAbsolute))
            {
                board.TryGetNode(index, out Node node);
                nodes.Add(node);
            }
            
            return nodes;

            Vector2Int AsAbsolute(Vector2Int position) => position.Add(other: Vector2Int.one * _range).ToInt();
        }
        
        public List<Node> GetMovement(Board board, Node origin)
        {
            List<Node> nodes = new();
            
            foreach (Vector2Int position in _positions)
            {
                Node currentNode = origin;
                do
                {   
                    currentNode = GetNode();
                    
                    if(!CanContinue())
                        break;
                    
                    nodes.Add(currentNode);
                        
                } while (IsDirectional);

                continue;

                bool CanContinue() 
                    => currentNode != null && 
                       currentNode.IsEmpty;

                Node GetNode()
                {
                    if(IsDirectional)
                        return currentNode.GetNeighbour(position);
                    
                    Vector2Int index = origin.Index + position;
                    board.TryGetNode(index, out Node node);
                    return node;
                }
            }
            
            return nodes;
        }
    }
}
