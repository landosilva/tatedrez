using System;
using System.Collections.Generic;
using System.Linq;
using Lando.Extensions;
using Lando.Plugins.Singletons.ScriptableObject;
using Tatedrez.Entities;
using UnityEditor;
using UnityEngine;

namespace Tatedrez.Data
{   
    [CreateAssetMenu(fileName = "PiecesDatabase", menuName = "Tatedrez/Databases/Pieces")]
    public class PiecesDatabase : Singleton<PiecesDatabase>
    {
        [SerializeField] private StyleData[] _styles;

        public static Sprite GetSprite(Piece.Style style, Piece.Type type)
        {
            return Instance._styles.First(MatchStyle).GetSprite(type);
            bool MatchStyle(StyleData styleData) => styleData.Style == style;
        }

        [Serializable]
        public class StyleData
        {
            [SerializeField] private Piece.Style _style;
            [SerializeField] private Texture2D _texture;

            [SerializeField, ReadOnly] private Sprite[] _sprites;
            
            public Piece.Style Style => _style;
            public bool IsInitialized => !_sprites.IsNullOrEmpty();

            public Sprite GetSprite(Piece.Type type)
            {
                Sprite sprite = _sprites[(int)type];
                if (sprite == null)
                    Debug.LogError($"Sprite for {type} is missing in {this}");
                return sprite;
            }
            
#if UNITY_EDITOR
            public string Path => AssetDatabase.GetAssetPath(_texture);
        
            public void Initialize()
            {
                if(_texture == null)
                    return;
            
                IEnumerable<Sprite> sprites = AssetDatabase.LoadAllAssetsAtPath(Path).OfType<Sprite>();
                _sprites = new Sprite[Enum.GetValues(typeof(Piece.Type)).Length];
                foreach (Sprite sprite in sprites)
                {
                    if(!Enum.TryParse(sprite.name, out Piece.Type type))
                        continue;
                
                    _sprites[(int)type] = sprite;
                }
            }
#endif
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(Application.isPlaying) 
                return;
            
            if(_styles == null) 
                return;
            
            foreach (StyleData styleData in _styles)
            {
                if(styleData.IsInitialized)
                    continue;
                
                styleData.Initialize();
            }
        }
#endif
    }
}
