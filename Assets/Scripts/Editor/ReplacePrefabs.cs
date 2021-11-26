using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplacePrefabs
{
    [MenuItem("Tools/Replace Prefabs")]
    private static void ReplaceCoinPrefabs()
    {
        GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("coin");
        foreach(GameObject old in oldObjects)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Collectibles/Coin.prefab") as GameObject);
            newObject.transform.position = old.transform.position;
            newObject.transform.rotation = old.transform.rotation;
            newObject.transform.parent = old.transform.parent;

            Object.DestroyImmediate(old);
        }
    }
}
