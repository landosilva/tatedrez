using Lando.Core.Extensions;
using UnityEngine;

namespace Tatedrez.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToUnits(this Vector2Int pixels) 
            => new(pixels.x * Constants.PixelToUnit, pixels.y * Constants.PixelToUnit);
        
        public static Vector2Int ToPixels(this Vector3 units)
            => units.Multiply(Constants.PixelsPerUnit).ToInt();
    }
}
