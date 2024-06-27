using Lando.Core;
using Lando.Core.Extensions;
using UnityEditor;
using UnityEngine;

namespace Lando.Plugins.Generators.Editor.Layer
{
    [CreateAssetMenu(fileName = "Layer Settings", menuName = "LightningRod/Generators/Settings/Layer")]
    public class LayerSettings : ScriptableObject
    {
        [SerializeField] private string _className = "Layer";
        [SerializeField] private string _path = "Scripts/Generated";
        [SerializeField] private string _namespace;
        
        public static string ClassName => _instance._className;
        public static string Path => "Assets/" + _instance._path;
        public static string Namespace => _instance._namespace;

        public static bool HasNamespace => !string.IsNullOrEmpty(Namespace);

        private static LayerSettings _instance;

        private void OnEnable()
        {
            PreloadedAssets.Add(this);
            _instance = this;
        }

        public void UpdatePathAndNamespace()
        {
            string productName = PlayerSettings.productName.ReplaceWhitespace();
            string[] list = _path.Split('/');
            list[0] = productName;
            _path = string.Join('/', list);
            _namespace = productName;
        }
    }
}
