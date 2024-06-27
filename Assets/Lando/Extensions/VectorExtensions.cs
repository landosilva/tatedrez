using Tatedrez;
using UnityEngine;

namespace Lando.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;
    }
    
    public static class ColorExtensions
    {
        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null) 
            => new(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
    }
    
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector2 vector, float? x = null, float? y = null, float? z = null) 
            => new(x ?? vector.x, y ?? vector.y, z ?? 0);
        public static Vector3 With(this Vector2Int vector, float? x = null, float? y = null, float? z = null) 
            => new(x ?? vector.x, y ?? vector.y, z ?? 0);
        
        public static Vector2 Add(this Vector2Int vector, Vector2 other) 
            => new(vector.x + other.x, vector.y + other.y);
        public static Vector3 Add(this Vector3 vector, Vector3 other)
            => new(vector.x + other.x, vector.y + other.y, vector.z + other.z);
        
        public static Vector3 Multiply(this Vector3 vector, float multiplier)
            => new(vector.x * multiplier, vector.y * multiplier, vector.z * multiplier);
        
        public static Vector2Int Divide(this Vector2Int vector, int divisor) 
            => (vector.ToFloat() / divisor).ToInt();
        
        public static Vector2 ToUnits(this Vector2Int pixels) 
            => new(pixels.x * Constants.PixelToUnit, pixels.y * Constants.PixelToUnit);
        
        public static Vector2Int ToPixels(this Vector3 units)
            => units.Multiply(Constants.PixelsPerUnit).ToInt();
        
        public static Vector2Int ToInt(this Vector3 vector)
            => new(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        public static Vector2Int ToInt(this Vector2 vector)
            => new(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        
        public static Vector2 ToFloat(this Vector2Int vector)
            => new(vector.x, vector.y);
    }
}