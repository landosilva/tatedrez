#if UNITY_EDITOR
using Lando.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public partial class Board
    {
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
#endif
