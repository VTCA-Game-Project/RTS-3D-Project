using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetUtils : MonoBehaviour
{
    private Dictionary<int, Object> assets = new Dictionary<int, Object>();
    private void Awake()
    {
        Object[] allAssets = Resources.LoadAll("");
        for (int i = 0; i < allAssets.Length; i++)
        {
            assets.Add(allAssets[i].name.GetHashCode(), allAssets[i]);
        }
    }

    public Object GetAsset(string name)
    {
        Object asset = null;
        assets.TryGetValue(name.GetHashCode(), out asset);
#if UNITY_EDITOR
        if (asset == null) Debug.Log("Not found asset");
#endif
        return asset;

    }
}
