using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor
{
    [CustomEditor(typeof(Settings), editorForChildClasses: true)]
    public class SettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Settings settings = (Settings) target;
            if (GUILayout.Button(text: "Generate")) 
                settings.Generate();
        }
    }
}