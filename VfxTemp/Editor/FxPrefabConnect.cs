using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BlackJack.ProjectF.Editor
{
    /// <summary>
    /// 重新关联预制体
    /// </summary>
    public class FxPrefabConnect : EditorWindow
    {
        private FxPrefabConnect()
        {
            this.titleContent = new GUIContent("Reconnect Objects To Prefab");
        }

        [MenuItem("ProjectF/特效工具/关联预制体 (选择要重新关联的物件和Project中的一个目标预制体)")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<FxPrefabConnect>();
            GetWindowWithRect(typeof(FxPrefabConnect), new Rect(0, 0, 200, 100));
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("1.选择物件(可多选)，在Project内加选目标预制体，执行。", EditorStyles.wordWrappedLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("执行", EditorStyles.miniButtonLeft))
            {
                ReConnectPrefab();
            }
            
            if (GUILayout.Button("取消", EditorStyles.miniButtonRight))
            {
                this.Close();
            }

            GUILayout.EndHorizontal();
            

            EditorGUILayout.LabelField("PS.只有当物体处在Default层时会被更改", EditorStyles.wordWrappedLabel);
        }

        private static void ReConnectPrefab()
        {
            //警告字段
            if (!EditorUtility.DisplayDialog("Prefab Reconnect Warning",
                "此操作会使选中的一个或多个物件重新关联一个目标预制体，并重置所有参数到预制体默认值，操作不可回撤，确定要执行吗？",
                "是的，确定执行",
                "不，取消操作"))
            {
                return;
            }

            ReConnectPrefabImpl(Selection.gameObjects);
        }

        private static void ReConnectPrefabImpl(GameObject[] selects)
        {
            List<GameObject> sourcelist = new List<GameObject>();
            var targetObj = selects[0];
            //var sourceObj = selects[0];
            foreach (var selobj in selects)
            {
                var getType = PrefabUtility.GetPrefabAssetType(selobj);

                if (getType == PrefabAssetType.NotAPrefab)
                {
                    sourcelist.Add(selobj);
                    //sourceObj = objtmp;
                    Debug.Log("Source is : " + selobj);
                    Debug.Log("Type is : " + getType + "......");
                }

                if (getType == PrefabAssetType.Regular | getType == PrefabAssetType.Variant)
                {
                    targetObj = selobj;
                    Debug.Log("Target is : " + selobj);
                    Debug.Log("Type is : " + getType + "......");
                }
            }

            if (targetObj && sourcelist.Count > 0)
            {
                DoReconnect(sourcelist, targetObj);
            }
        }

        private static void DoReconnect(List<GameObject> srcObjs, GameObject targetPrefab)
        {
            foreach (var selobj in srcObjs)
            {
                Debug.Log("Object >>" + selobj + "<< is Reconnect... to the Prefab >>" + targetPrefab + "<< Done!!!");
#pragma warning disable 0618
                PrefabUtility.ConnectGameObjectToPrefab(selobj, targetPrefab);
#pragma warning restore 0618
            }

            Debug.Log(srcObjs.Count + " Objects connected!!");
        }


    }
}
