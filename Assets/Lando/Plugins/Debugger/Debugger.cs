using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Lando.Core.Extensions;
using Lando.Plugins.Singletons.ScriptableObject;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lando.Plugins.Debugger
{
    [CreateAssetMenu(menuName = "Tatedrez/Managers/Debugger", fileName = "Debugger")]
    public class Debugger : Singleton<Debugger>
    {   
        [SerializeField] private bool _enabled = true;
        [SerializeField] private List<DebugSource> _sources = new();
        
        public List<DebugSource> Sources => _sources;
        
        private const float COLOR_THRESHOLD = 0.2f;

        public static void Enable() => Instance._enabled = true;
        public static void Disable() => Instance._enabled = false;
        
        public static void Log(object log)
        {
            if (!Instance._enabled) 
                return;

            StackTrace stackTrace = new();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            if (stackFrames == null || stackFrames.Length < 2)
            {
                Debug.Log(log);
                return;
            }
            
            StackFrame frame = stackFrames[1];
            MethodBase method = frame.GetMethod();
            Type declaringType = method.DeclaringType;
            if (declaringType == null)
                return;

            string key = $"{Clean(declaringType.Name)}.{Clean(method.Name)}";

            Instance.AddOrGet(key, out DebugSource source);
            if (!source.Enabled) 
                return;

            string prefix = $"[{key.ToColor(source.Color).ToBold()}]: ";
            Debug.Log($"{prefix}{log}");
        }

        private static string Clean(string input)
        {
            input = Regex.Replace(input, pattern: "g__", replacement: ".");
            
            const string pattern = "[<>|0-9_]|c__";
            const string replacement = "";
            string result = Regex.Replace(input, pattern, replacement);
            return result;
        }
        
        private void AddOrGet(string source, out DebugSource debugSource)
        {
            debugSource = _sources.Find(Match);
            if (debugSource != null) 
                return;
            
            Color color = GetRandomVisibleColor();
            debugSource = new DebugSource(name: source, color);
            _sources.Add(debugSource);

            return;

            bool Match(DebugSource o) => o.Name == source;
        }

        private static Color GetRandomVisibleColor()
        {
#if UNITY_EDITOR
            bool isDarkMode = EditorGUIUtility.isProSkin;
#else
            bool isDarkMode = false;
#endif

            const float saturation = 0.7f;
            float value = isDarkMode ? 0.8f : 0.6f;
            Color newColor;
            int attempts = 0;
            do
            {
                float hue = Random.Range(0f, 1f);
                newColor = Color.HSVToRGB(hue, saturation, value);
                attempts++;
            }
            while (!IsColorDistinct(newColor, Instance._sources, COLOR_THRESHOLD) && attempts < 100);

            return newColor;
        }

        private static bool IsColorDistinct(Color newColor, List<DebugSource> sources, float threshold)
        {
            foreach (DebugSource source in sources)
            {
                if (ColorDistance(newColor, source.Color) < threshold)
                    return false;
            }
            return true;
        }

        private static float ColorDistance(Color a, Color b)
        {
            float r = a.r - b.r;
            float g = a.g - b.g;
            float bl = a.b - b.b;
            return Mathf.Sqrt(r * r + g * g + bl * bl);
        }

        [Serializable]
        public class DebugSource
        {
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public Color Color { get; set; }
            [field: SerializeField] public bool Enabled { get; set; } = true;
            
            public DebugSource(string name, Color color)
            {
                Name = name;
                Color = color;
            }
        }
    }
}