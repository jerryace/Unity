using UnityEngine;
using UnityEditor;

namespace BlackJack.ProjectF.Editor
{
    /// <summary>
    /// 嵌套预制体保存 Transform组件右键功能
    /// </summary>
    public class FxNestedPrefabSave
    {
        [MenuItem("GameObject/ProjectF/Save Nested Prefab")]
        public static void SaveNestPrefab()
        {
            GameObject originGo = Selection.activeGameObject;
            string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(originGo);

            //通过PrefabUtility查找当前选中物件最近的prefabGameObjectRoot。Duplicate要对这个root操作
            Selection.activeObject = PrefabUtility.GetNearestPrefabInstanceRoot(Selection.activeGameObject);
            Unsupported.DuplicateGameObjectsUsingPasteboard();
            
            GameObject duplicate = Selection.activeGameObject;
            PrefabUtility.SaveAsPrefabAssetAndConnect(duplicate, path, InteractionMode.AutomatedAction, out bool pfbSuccess);
            Object.DestroyImmediate(duplicate);

            Selection.activeGameObject = originGo;

            if (pfbSuccess == false)
            {
                Debug.Log("保存失败");
                return;
            }
            Debug.Log("保存完成");
        }
    }
}
