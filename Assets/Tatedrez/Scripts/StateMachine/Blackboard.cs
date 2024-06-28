using System.Collections.Generic;
using UnityEngine;

namespace Tatedrez.States
{
    public class Blackboard : MonoBehaviour
    {
        private readonly Dictionary<string, object> _data = new();
        
        public void Set<T>(string key, T value) => _data[key] = value;
        public void Set<T>(T value) => Set(typeof(T).Name, value);
        
        
        public T Get<T>(string key) => (T)_data[key];
        public T Get<T>() => Get<T>(typeof(T).Name);
    }
}