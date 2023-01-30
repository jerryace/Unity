using UnityEngine;
using UnityEditor;

public class ParticleNoiseAttribute : EditorWindow
{
    private static ParticleNoiseAttribute m_editor_ex;

    [MenuItem("YaoFx/ParticleSys/NoiseAttribute/Open",false,401)]
    static void OpenEditor()
    {
        if (m_editor_ex == null)
        {
            m_editor_ex = EditorWindow.GetWindow<ParticleNoiseAttribute>("EffectEditorEx", true);
            m_editor_ex.autoRepaintOnSceneChange = true;
        }
        m_editor_ex.Show(true);
    }

    private string m_modifiedNames;

    private void OnGUI()
    {
        GUILayout.Label("Noise Quality:");

        if (GUILayout.Button("Low"))
        {
            SetParticleNoiseQuality(ParticleSystemNoiseQuality.Low);
        }
        if (GUILayout.Button("Medium"))
        {
            SetParticleNoiseQuality(ParticleSystemNoiseQuality.Medium);
        }
        if (GUILayout.Button("High"))
        {
            SetParticleNoiseQuality(ParticleSystemNoiseQuality.High);
        }

        m_modifiedNames = EditorGUILayout.TextArea(m_modifiedNames);
    }

    private void SetParticleNoiseQuality(ParticleSystemNoiseQuality quality)
    {
        var sb = new System.Text.StringBuilder();
        var items = Selection.GetFiltered<ParticleSystem>(SelectionMode.ExcludePrefab);
        foreach (var item in items)
        {
            var maindata = item.main;
            var name = item.name;
            var noise = item.noise;    
            if(noise.enabled == true){
                Debug.Log($"{name}'s Noise Quality set to {quality}！");
                noise.quality = quality;

                sb.AppendLine(name);
            }
        }

        m_modifiedNames = sb.ToString();
    }
}