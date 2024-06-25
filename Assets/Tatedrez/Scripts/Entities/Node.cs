using UnityEngine;

namespace Tatedrez.Entities
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;
        private Piece _piece;

        private void Awake()
        {
            _highlight.SetActive(false);
        }
    }
}