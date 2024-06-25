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
        
        public static Vector2 ToUnits(this Vector2Int vector) =>
            new(vector.x * Constants.PixelToUnit, vector.y * Constants.PixelToUnit);
    }
}