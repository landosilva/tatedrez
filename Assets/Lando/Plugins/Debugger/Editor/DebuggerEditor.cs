using System.Linq;
using Lando.Core.Extensions;
using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Debugger.Editor
{
    [CustomEditor(typeof(Debugger))]
    public class DebuggerEditor : UnityEditor.Editor
    {
        private static readonly Color ActiveColor = Color.white.With(r: 0.4f, g: 1f, b: 0.4f);
        private static readonly Color InactiveColor = Color.white.With(r: 1f, g: 0.6f, b: 0.6f);
        
        public override void OnInspectorGUI()
        {
            Debugger debugger = (Debugger)target;

            GUILayout.Space(pixels: 10);
            
            const float nameWidth = 200;
            const float colorWidth = 70;
            const float enabledWidth = 50;
            const float trashButtonWidth = 32;
            
            GUILayoutOption[] nameOptions = {GUILayout.Width(nameWidth), GUILayout.ExpandWidth(true)};
            GUILayoutOption[] colorOptions = {GUILayout.Width(colorWidth)};
            GUILayoutOption[] enabledOptions = {GUILayout.Width(enabledWidth)};
            GUILayoutOption[] trashButtonOptions = {GUILayout.Width(trashButtonWidth)};
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(text: "Name", EditorStyles.boldLabel, nameOptions);
            GUILayout.Label(text: "Color", EditorStyles.boldLabel, colorOptions);
            GUILayout.Label(text: "Enabled", EditorStyles.boldLabel, enabledOptions);
            GUILayout.Label(text: "", EditorStyles.boldLabel, trashButtonOptions);
            EditorGUILayout.EndHorizontal();
            
            foreach (Debugger.DebugSource source in debugger.Sources.ToList())
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(source.Name, nameOptions);
                source.Color = EditorGUILayout.ColorField(source.Color, colorOptions);
                GUI.backgroundColor = source.Enabled ? ActiveColor : InactiveColor;
                if (GUILayout.Button(source.Enabled ? "✓" : "╳", enabledOptions))
                    source.Enabled = !source.Enabled;
                GUI.backgroundColor = Color.white;
                
                GUIContent trashIcon = EditorGUIUtility.IconContent(name: "TreeEditor.Trash");
                if (GUILayout.Button(trashIcon, trashButtonOptions))
                    debugger.Sources.Remove(source);
                
                EditorGUILayout.EndHorizontal();
            }
            
            if (GUILayout.Button(text: "Clear")) 
                debugger.Sources.Clear();
            
            if (GUI.changed) 
                EditorUtility.SetDirty(target);
        }
    }
}