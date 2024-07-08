using System.Collections.Generic;
using System.IO;
using System.Text;
using Lando.Core.Extensions;
using Lando.Core.Utilities;
using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor.Layer
{
    public abstract class Generator : ScriptableObject
    {
        public abstract void Generate(Settings settings);
    }
    
    [CreateAssetMenu(fileName = "Layer Generator", menuName = "Lando/Generators/Layer")]
    public class LayerGenerator : Generator
    {
        public override void Generate(Settings settings)
        {
            List<LayerData> layersData = GetAllLayersData();
            StringBuilder stringBuilder = new();

            int identLevel = 0;

            if (settings.HasNamespace)
            {
                AddLine($"namespace {settings.Namespace}");
                OpenCurlyBrackets();
            }
            
            AddLine($"public static class {settings.ClassName}");
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
            
            string fileName = string.Concat(settings.ClassName, ".cs");

            if (!Directory.Exists(settings.Path))
                Directory.CreateDirectory(settings.Path);
            
            string fullPath = string.Concat(settings.Path, "/", fileName);
            string contents = stringBuilder.ToString();
            
            File.WriteAllText(fullPath, contents);
            
            AssetDatabase.ImportAsset(fullPath);
            
            return;

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
                Name = _originalName.ReplaceWhitespace();
                Layer = layer;
            }
        }
    }
}
