using UnityEditor;

public class ReadMe : EditorWindow
{
    [MenuItem("ProjectF/特效工具/Asset Reference Find/How To...")]
    static void HowTo()
    {
        //弹出窗口
        //EditorWindow.GetWindow(typeof(CustomVFX));
        //弹出对话框内容
        EditorUtility.DisplayDialog("How to Use", "1.选中Asset，右键--Find References In Project." + "\r\n" + "1.1.ProjectF/FX/Asset Reference Finder手动打开Tool窗口." + "\r\n" + "2.Refresh Data 刷新数据." + "\r\n" + "3.Model(Depend)显示使用了哪些资源." + "\r\n" + "4.Model(Reference)显示被哪些资源引用." + "\r\n" + "5.Need Update State 显示是否需要更新状态." + "\r\n" + "6.双击列表内Asset快速定位", "Yes");
    }
}

