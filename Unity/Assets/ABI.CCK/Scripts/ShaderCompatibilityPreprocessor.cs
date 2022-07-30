#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using System.Text;

public class ShaderCompatibilityPreprocessor : IPreprocessShaders
{
    public int callbackOrder => 1024;

    private static ShaderKeyword _singlePassKeyword = new ShaderKeyword("UNITY_SINGLE_PASS_STEREO");
    private static ShaderKeyword _instancedKeyword = new ShaderKeyword("STEREO_INSTANCING_ON");
    
    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
    {
        if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
        {
            List<ShaderCompilerData> reworkedData = new List<ShaderCompilerData>();
            foreach(ShaderCompilerData scd in data)
            {
                ShaderCompilerData reworkedScd = scd;
                ShaderKeywordSet shaderKeywordSet = scd.shaderKeywordSet;
                if(shaderKeywordSet.IsEnabled(_singlePassKeyword))
                {
                    shaderKeywordSet.Disable(_singlePassKeyword);
                    shaderKeywordSet.Enable(_instancedKeyword);
                    reworkedScd.shaderKeywordSet = shaderKeywordSet;
                    reworkedData.Add(reworkedScd);
                    continue;
                }
                
                if(shaderKeywordSet.IsEnabled(_instancedKeyword))
                {
                    shaderKeywordSet.Enable(_singlePassKeyword);
                    shaderKeywordSet.Disable(_instancedKeyword);
                    reworkedScd.shaderKeywordSet = shaderKeywordSet;
                    reworkedData.Add(reworkedScd);
                    continue;
                }
            }

            foreach(ShaderCompilerData entry in reworkedData)
            {
                data.Add(entry);
            }
        }
    }
}
#endif