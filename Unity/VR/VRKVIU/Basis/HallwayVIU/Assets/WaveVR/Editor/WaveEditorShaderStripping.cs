#if UNITY_2018_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

[InitializeOnLoad]
static class WaveEditorShaderStripping
{
    static WaveEditorShaderStripping()
    {
        isShaderStrippingEnabled = EditorPrefs.GetBool("WaveShaderStripping", false);
        Menu.SetChecked("WaveVR/Enable Shader Stripping", isShaderStrippingEnabled);
    }

    static bool isShaderStrippingEnabled = false;

    [MenuItem("WaveVR/Enable Shader Stripping", false, 2)]
    private static void SetShaderStrippingEnable()
    {
        isShaderStrippingEnabled = !isShaderStrippingEnabled;
        Menu.SetChecked("WaveVR/Enable Shader Stripping", isShaderStrippingEnabled);
        EditorPrefs.SetBool("WaveShaderStripping", isShaderStrippingEnabled);
    }

    private class ShaderStrippingBuildProcessor : IPreprocessShaders
    {
        public int callbackOrder { get { return 0; } }

        public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
        {
            isShaderStrippingEnabled = EditorPrefs.GetBool("WaveShaderStripping", false);
            Menu.SetChecked("WaveVR/Enable Shader Stripping", isShaderStrippingEnabled);

            if (!isShaderStrippingEnabled) return;

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                Debug.Log("WaveEditorShaderStripping: Stripping Shaders");

                List<GraphicsTier> stripTierList = new List<GraphicsTier>();
                stripTierList.Add(GraphicsTier.Tier1);
                stripTierList.Add(GraphicsTier.Tier3);

                for (int i = data.Count - 1; i >= 0; --i)
                {
                    if (stripTierList.Contains(data[i].graphicsTier))
                    {
                        //Debug.Log("WaveEditorShaderStripping: Remove Shader at " + i);
                        data.RemoveAt(i);
                    }
                }
            }
        }
    }
}

#endif
