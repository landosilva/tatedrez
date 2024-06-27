using System.Collections.Generic;
using Tatedrez.Entities;
using UnityEngine;

namespace Tatedrez.Data
{
    [CreateAssetMenu(fileName = "Movement", menuName = "Tatedrez/Movement")]
    public partial class Movement : ScriptableObject
    {   
        [SerializeField] private List<Vector2Int> _positions;
        
        private bool IsDirectional => _range <= 1;
        
        public List<Node> GetNodes(Board board, Node origin)
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
