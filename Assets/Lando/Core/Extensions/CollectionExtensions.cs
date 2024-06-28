using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lando.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;
        
        public static T PickRandom<T>(this IEnumerable<T> collection)
        {
            if (!collection.Any())
                return default;
            
            int randomIndex = Random.Range(0, collection.Count());
            return collection.ElementAt(randomIndex);
        }
    }
}