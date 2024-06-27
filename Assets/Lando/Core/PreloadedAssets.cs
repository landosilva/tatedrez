using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lando.Core
{
    public static class PreloadedAssets
    {   
        public static void Add(Object asset) 
        {
            List<Object> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();

            bool isAlreadyPreloaded = preloadedAssets.Any(IsEqualsToThis); 
            if(isAlreadyPreloaded)
                return;

            preloadedAssets.Add(asset);
            
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            
            return;

            bool IsEqualsToThis(Object preloadedAsset)
            {
                return preloadedAsset != null &&
                       preloadedAsset.GetInstanceID() == asset.GetInstanceID();
            }
        }
        
        public static void RemoveEmpties()
        {
            Object[] preloadedAssets = NonNullPreloadedAssets();
            PlayerSettings.SetPreloadedAssets(preloadedAssets);
            
            return;

            Object[] NonNullPreloadedAssets() => PlayerSettings.GetPreloadedAssets().Where(NonNull).ToArray();
            bool NonNull(Object preloadedAsset) => preloadedAsset != null;
        }
    }
}