using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lando.Core.Extensions;
using Lando.Core.Utilities;
using Lando.Plugins.Generators.Editor.Layer;
using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor.Scene
{
    [CreateAssetMenu(fileName = "Scene Generator", menuName = "Lando/Generators/Scene")]
    public class SceneGenerator : Generator
    {
        public override void Generate(Settings settings)
        {
            List<string> scenes = GetAllScenes();
            StringBuilder stringBuilder = new();

            int identLevel = 0;

            if (settings.HasNamespace)
            {
                AddLine($"namespace {settings.Namespace}");
                OpenCurlyBrackets();
            }
            
            AddLine($"public static class {settings.ClassName}");
            OpenCurlyBrackets();
            
            int index = 0;
            foreach (string sceneName in scenes)
            {
                AddLine($"public static int {sceneName} = {index};");
                index++;
            }

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

        private static List<string> GetAllScenes()
        {
            return EditorBuildSettings.scenes.Select(SceneNames).ToList();
            string SceneNames(EditorBuildSettingsScene scene) => 
                Path.GetFileNameWithoutExtension(scene.path)
                    .ReplaceWhitespace()
                    .ReplaceNumbers();
        }
    }
}