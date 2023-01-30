using UnityEngine;
using UnityEditor;

public class ParticleMaxsizeMatchRenderlayer : EditorWindow
{
    private static ParticleMaxsizeMatchRenderlayer m_editor_ex;

    [MenuItem("YaoFx/ParticleSys/MaxsizeMatchRenderlayer/Open", false,401)]
    static void OpenEditor()
    {
        if (m_editor_ex == null)
        {
            m_editor_ex = EditorWindow.GetWindow<ParticleMaxsizeMatchRenderlayer>("EffectEditorEx", true);
            m_editor_ex.autoRepaintOnSceneChange = true;
        }
        m_editor_ex.Show(true);
    }

    private string m_modifiedNames;

    private void OnGUI()
    {
        GUILayout.Label("RenderLayer:MaxParticleSize");
        GUILayout.Label("PC Only : 1.0");
        GUILayout.Label("High : 0.8");
        GUILayout.Label("Medium : 0.6");
        GUILayout.Label("Default : 0.5");
        GUILayout.Label("LD : 0.4");
        GUILayout.Label("All CullingMode is Automatic");

        if (GUILayout.Button("Run"))
        {
            var items = Selection.GetFiltered<ParticleSystem>(SelectionMode.ExcludePrefab);
            foreach (var item in items)
            {
                var maindata = item.main;
                var name = item.name;
                var rendmodule = item.GetComponent<ParticleSystemRenderer>();
                maindata.cullingMode = ParticleSystemCullingMode.Automatic;

                if (rendmodule.enabled == true)
                {
                    var layermask = rendmodule.renderingLayerMask;
                    var rendermode = rendmodule.renderMode;
                    
                    if (rendermode != ParticleSystemRenderMode.Mesh)
                    {
                        Debug.Log(name + " RenderMode is: " + rendermode);
                        
                        if (layermask == 1)
                        {
                            rendmodule.maxParticleSize = 0.5f;
                            Debug.Log(name + ": renderingLayermask Default, maxParticleSize set to 0.5");
                        }
                        if (layermask == 2)
                        {
                            rendmodule.maxParticleSize = 0.6f;
                            Debug.Log(name + ": renderingLayermask Medium, maxParticleSize set to 0.6");
                        }
                        if (layermask == 4)
                        {
                            rendmodule.maxParticleSize = 0.8f;
                            Debug.Log(name + ": renderingLayermask High, maxParticleSize set to 0.8");
                        }
                        if (layermask == 8)
                        {
                            rendmodule.maxParticleSize = 1f;
                            Debug.Log(name + ": renderingLayermask PC Only, maxParticleSize set to 1");
                        }
                        if (layermask == 16)
                        {
                            rendmodule.maxParticleSize = 0.4f;
                            Debug.Log(name + ": renderingLayermask LD, maxParticleSize set to 0.4");
                        }
                    }
                }
            }
        }
    }

}