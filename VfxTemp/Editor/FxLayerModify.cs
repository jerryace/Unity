
using UnityEditor;
using UnityEngine;
//using UnityEditor.IMGUI.Controls;

namespace BlackJack.ProjectF.Editor
{
    /// <summary>
    /// 批量修改预制体layer.
    /// </summary>
    public class FxLayerModify : EditorWindow
    {
        private int m_sel;

        private FxLayerModify()
        {
            this.titleContent = new GUIContent("Prefab Layer Change");
        }

        [MenuItem("ProjectF/特效工具/Batch gameObjects Layer Change")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<FxLayerModify>();
            GetWindowWithRect(typeof(FxLayerModify), new Rect(0, 0, 300, 150));
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("1.批量选择物体，选TargetLayer，选执行，批量自动修改。", EditorStyles.wordWrappedLabel);

            //TargeLayer中选择想要切换去的Layer
            //选择取消按钮则退出关闭窗口
            //选择执行按钮则执行切换层级

            m_sel = EditorGUILayout.LayerField("Target layer", m_sel);
            
            if (GUILayout.Button("执行"))
            {
                GameObject[] sel = Selection.gameObjects;

                if (sel != null)
                {
                    ListApply(sel, m_sel);
                }
            }
            
            if (GUILayout.Button("取消"))
            {
                Close();
            }


            EditorGUILayout.LabelField("PS.只有当物体处在Default层时会被更改", EditorStyles.wordWrappedLabel);
        }

        //遍历所有选中Object，并执行Layer切换
        private static void ListApply(GameObject[] objs, int layer)
        {
            foreach (var item in objs)
            {
                ApplyLayers(item, layer);
            }
        }

        //切换指定物件及其所有子层级的Layer，如果不是Default则保持不变
        private static void ApplyLayers(GameObject obj, int layer)
        {
            if (obj.layer == 0)
            {
                obj.layer = layer;
            }
            
            foreach (Transform child in obj.transform)
            {
                ApplyLayers(child.gameObject, layer);
            }
        }
    }
}

