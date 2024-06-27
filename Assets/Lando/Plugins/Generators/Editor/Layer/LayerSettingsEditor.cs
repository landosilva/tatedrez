using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor.Layer
{
    [CustomEditor(typeof(LayerSettings))]
    public class LayerSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            LayerSettings layerSettings = (LayerSettings) target;

            if (GUILayout.Button("Update Path & Namespace"))
            {
                layerSettings.UpdatePathAndNamespace();
                EditorUtility.SetDirty(layerSettings);
                AssetDatabase.SaveAssets();
            }
            
            if (GUILayout.Button("Generate")) 
                LayerGenerator.Generate();
        }
    }
}