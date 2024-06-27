using System.Collections.Generic;
using System.IO;
using System.Text;
using LightningRod.Utilities;
using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor.Layer
{
    public static class LayerGenerator
    {
        [MenuItem("Tools/Lightning Rod/Generators/Layer Class")]
        public static void Generate()
        {
            List<LayerData> layersData = GetAllLayersData();
            StringBuilder stringBuilder = new();

            int identLevel = 0;

            if (LayerSettings.HasNamespace)
            {
                AddLine($"namespace {LayerSettings.Namespace}");
                OpenCurlyBrackets();
            }
            
            AddLine($"public static class {LayerSettings.ClassName}");
            OpenCurlyBrackets();
            
            foreach (LayerData layerData in layersData) 
                AddLine($"public static int {layerData.Name} = {layerData.Layer};");

            EmptyLine();
            
            AddLine("public static class Mask");
            OpenCurlyBrackets();
            
            foreach (LayerData layerData in layersData) 
                AddLine($"public static int {layerData.Name} = 1 << {layerData.Layer};");

            CloseCurlyBrackets();
            CloseCurlyBrackets();
            CloseCurlyBrackets();
            
            string fileName = string.Concat(LayerSettings.ClassName, ".cs");

            if (!Directory.Exists(LayerSettings.Path))
                Directory.CreateDirectory(LayerSettings.Path);
            
            string fullPath = string.Concat(LayerSettings.Path, "/", fileName);
            string contents = stringBuilder.ToString();
            
            File.WriteAllText(fullPath, contents);
            
            AssetDatabase.ImportAsset(fullPath);
            
            string Ident(int level) => StringUtilities.Indent(level);
            void OpenCurlyBrackets() => stringBuilder.AppendLine($"{Ident(identLevel++)}{{");
            void CloseCurlyBrackets() => stringBuilder.AppendLine($"{Ident(--identLevel)}}}");
            void AddLine(string line) => stringBuilder.AppendLine($"{Ident(identLevel)}{line}");
            void EmptyLine() => AddLine("");
        }

        private static List<LayerData> GetAllLayersData()
        {
            List<LayerData> layers = new();
            
            for (int layer = 0; layer < 31; layer++)
            {
                LayerData layerData = new (layer);
                if(layerData.IsValid)
                    continue;
                
                layers.Add(layerData);
            }
            
            return layers;
        }

        private class LayerData
        {
            public readonly string Name;
            public readonly int Layer;

            private readonly string _originalName;
            
            public bool IsValid => string.IsNullOrEmpty(_originalName);
            
            internal LayerData(int layer)
            {
                _originalName = LayerMask.LayerToName(layer);
                Name = StringUtilities.ReplaceWhitespace(_originalName);
                Layer = layer;
            }
        }
    }
}
