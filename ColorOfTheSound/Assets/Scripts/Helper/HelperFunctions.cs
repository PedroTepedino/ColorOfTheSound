using UnityEditor;
using UnityEngine;

public static class HelperFunctions
{
    public static T FindDefaultParameterAssetOfType<T>(string typeFilter = null) where T : ScriptableObject
    {
        if (typeFilter == null)
        {
            typeFilter = typeof(T).FullName;
        }
        
        var bombs = AssetDatabase.FindAssets($"t:{typeFilter}", new []{"Assets/ScriptableObjects"});

        foreach (var bomb in bombs)
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(bomb));
            
            if (asset == null) continue;
            
            return asset;
        }

        var defaultParameters = AssetDatabase.LoadAssetAtPath<T>($"Assets/ScriptableObjects/Default{typeFilter}.asset");
        if (defaultParameters != null)
        {
            return defaultParameters;
        }
        
        T createdParameters = (T)ScriptableObject.CreateInstance(typeof(T));
        AssetDatabase.CreateAsset(createdParameters, $"Assets/ScriptableObjects/Default{typeFilter}.asset");
        AssetDatabase.Refresh();
        
        return createdParameters;
    }
}
