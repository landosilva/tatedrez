using Lando.Plugins.Generators.Editor.Layer;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor
{   
    public class Settings : ScriptableObject
    {
        [SerializeField] private string _className = "Settings";
        [SerializeField] private string _path = "Scripts/Generated";
        [SerializeField] private string _namespace;
        [SerializeField] private Generator _generator;
        
        public string ClassName => _className;
        public string Path => "Assets/" + _path;
        public string Namespace => _namespace;
        public bool HasNamespace => !string.IsNullOrEmpty(Namespace);

        public void Generate() => _generator.Generate(settings: this);
    }
}