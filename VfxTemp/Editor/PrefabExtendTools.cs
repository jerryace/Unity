using UnityEngine;
using UnityEditor;
using System.Collections;

static public class PrefabExtendTools
{

    [MenuItem("CONTEXT/Transform/OverridePrefab")]
    static public void SavePrefab()
    {
        GameObject tarPrefab = PrefabUtility.GetCorrespondingObjectFromSource (Selection.activeGameObject) as GameObject;
        //if(tarPrefab == null) return;
        Debug.Log(tarPrefab);
        string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(tarPrefab);
        Debug.Log(prefabPath);
        if(prefabPath.EndsWith(".prefab") == false) return;
        AssetDatabase.LoadMainAssetAtPath(prefabPath);
        Debug.Log(AssetDatabase.LoadMainAssetAtPath(prefabPath));
        //PrefabUtility.ReplacePrefab (Selection.activeGameObject, , ReplacePrefabOptions.ConnectToPrefab | ReplacePrefabOptions.ReplaceNameBased);
        //PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        PrefabUtility.SaveAsPrefabAssetAndConnect(Selection.activeGameObject, prefabPath, InteractionMode.AutomatedAction);
    }
}